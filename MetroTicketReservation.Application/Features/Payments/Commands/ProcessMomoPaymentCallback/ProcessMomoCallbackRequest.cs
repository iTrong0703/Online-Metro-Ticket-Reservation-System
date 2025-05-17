using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPaymentCallback
{
    public class ProcessMomoCallbackRequest : IRequest<TicketInfoResponseDto>
    {
        public IQueryCollection QueryCollection { get; set; }
    }
}
