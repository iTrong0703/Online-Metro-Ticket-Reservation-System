using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPayment
{
    public class ProcessMomoPaymentRequest : IRequest<string>
    {
        public int PaymentId { get; set; }
    }
}
