using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPayment
{
    public class ProcessMomoPaymentRequestHandler : IRequestHandler<ProcessMomoPaymentRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMomoPaymentService _momoService;
        private readonly IConfiguration _configuration;

        public ProcessMomoPaymentRequestHandler(IUnitOfWork unitOfWork, IMomoPaymentService momoService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _momoService = momoService;
            _configuration = configuration;
        }
        public async Task<string> Handle(ProcessMomoPaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(request.PaymentId);
            if (payment == null)
            {
                throw new NotFoundException(nameof(payment), request.PaymentId.ToString());
            }

            if (payment.PaymentStatus != PaymentStatus.Pending)
            {
                throw new BusinessException("Payment was canceled or you have checkout successful!");
            }

            var momoRequest = new MomoPaymentRequest
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                OrderInfo = $"Payment for Metro Ticket #{payment.OrderId}",
            };

            var response = await _momoService.CreatePaymentAsync(momoRequest);

            
            if (response.ErrorCode != 0)
            {
                payment.PaymentStatus = PaymentStatus.Failed;
                payment.LastModifiedDate = DateTime.UtcNow;

                await _unitOfWork.SaveAllAsync();
                throw new BusinessException($"Momo payment failed: {response.Message}");
            }

            return response.PayUrl;
        }
    }
}
