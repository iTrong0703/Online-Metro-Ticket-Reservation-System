using MediatR;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.CreateTicketType
{
    public class CreateTicketTypeRequest : BaseTicketTypeRequest, IRequest<int>
    {
    }
}
