using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.Payments.Commands.CreatePayment;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPaymentCallback
{
    public class ProcessMomoCallbackRequestHandler : IRequestHandler<ProcessMomoCallbackRequest, TicketInfoResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMomoPaymentService _momoService;
        private readonly ICacheService _cacheService;

        public ProcessMomoCallbackRequestHandler(IUnitOfWork unitOfWork, IMomoPaymentService momoService, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _momoService = momoService;
            _cacheService = cacheService;
        }
        public async Task<TicketInfoResponseDto> Handle(ProcessMomoCallbackRequest request, CancellationToken cancellationToken)
        {
            var response = _momoService.ProcessPaymentCallback(request.QueryCollection);

            // Get orderId from response
            string orderId = response.OrderId;

            // Find payment by orderId
            var payment = await _unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
            if (payment == null)
            {
                throw new NotFoundException(nameof(payment), orderId);
            }

            payment.LastModifiedDate = DateTime.UtcNow;
            var ticketData = await _cacheService.GetAsync<TicketInfoDto>($"orderId:{payment.OrderId}");
            if (ticketData == null)
            {
                throw new Exception("Payment data not found");
            }
            Ticket? createdTicket = null;
            // response code from Momo: 0 = success
            // chưa sinh trắc học nên chữa cháy != 0 :D
            //if (response.ErrorCode == 0)
            if (response.ResponseCode != 0)
            {
                var ticketId = await CreateTicketForPassenger(ticketData);
                payment.TicketId = ticketId;
                payment.PaymentStatus = PaymentStatus.Completed;
                payment.PaymentDate = DateTime.UtcNow;
                createdTicket = await _unitOfWork.TicketRepository.GetTicketWithDetailsAsync(ticketId);
            }
            else
            {
                payment.PaymentStatus = PaymentStatus.Failed;
            }

            await _unitOfWork.SaveAllAsync();
            await _cacheService.RemoveAsync($"orderId:{payment.OrderId}");

            return new TicketInfoResponseDto
            {
                TicketId = createdTicket.TicketID,
                PassengerName = createdTicket.Passenger?.FullName ?? "N/A",
                TicketType = createdTicket.TicketType?.TicketName ?? "N/A",
                PurchaseDate = createdTicket.PurchaseDate,
                ExpiryDate = createdTicket.ExpiryDate,
                StartStationName = createdTicket.StartStation?.StationName,
                EndStationName = createdTicket.EndStation?.StationName
            };
        }
        private async Task<int> CreateTicketForPassenger(TicketInfoDto ticketInfo)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();


                var passenger = await _unitOfWork.Passengers.GetByIdAsync(ticketInfo.PassengerId);
                if (passenger == null)
                    throw new NotFoundException(nameof(passenger), ticketInfo.PassengerId.ToString());

                var ticket = new Ticket
                {
                    PassengerID = ticketInfo.PassengerId,
                    PurchaseDate = DateTime.UtcNow,
                    IsActive = true,
                    TicketTypeID = ticketInfo.TicketTypeId,
                    StartStationID = ticketInfo.StartStationId,
                    EndStationID = ticketInfo.EndStationId,
                    PurchaseChannel = "App"
                };

                await _unitOfWork.Tickets.AddAsync(ticket);
                await _unitOfWork.SaveAllAsync();
                await _unitOfWork.CommitTransactionAsync();
                return ticket.TicketID;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
