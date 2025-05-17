using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.StationFares.Commands.CreateStationFare;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public CreatePaymentRequestHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreatePaymentRequestValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }

            int amount = await CalculateTicketAmountAsync(
                request.TicketTypeId,
                request.StartStationId ?? 0,
                request.EndStationId ?? 0);

            string orderId = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}{Random.Shared.Next(1000, 9999)}";


            var ticketData = new TicketInfoDto
            {
                TicketTypeId = request.TicketTypeId,
                StartStationId = request.StartStationId,
                EndStationId = request.EndStationId,
                PassengerId = request.PassengerId
            };

            await _cacheService.SetAsync($"orderId:{orderId}", ticketData, TimeSpan.FromMinutes(10));

            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = PaymentStatus.Pending,
                PassengerId = request.PassengerId,
                CreatedDate = DateTime.UtcNow,
            };


            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveAllAsync();

            return payment.PaymentId;
        }
        public async Task<int> CalculateTicketAmountAsync(int ticketTypeId, int startStationId, int endStationId)
        {
            var ticketType = await _unitOfWork.TicketTypes.GetByIdAsync(ticketTypeId);

            if (ticketType == null)
                throw new BusinessException("TicketType not available");
            int amount = 0;
            if (!ticketType.IsTimeBased && startStationId > 0 && endStationId > 0)
            {
                var stationFare = await _unitOfWork.StationFareRepository.GetStationFareById(startStationId, endStationId);
                if (stationFare == null)
                {
                    throw new NotFoundException(nameof(stationFare), $"{startStationId}-{endStationId}");
                }

                amount = stationFare.Fare;
            } else
            {
                amount = ticketType.TicketPrice;
            }
            return amount;
        }

    }
}
