using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.DeleteTicketType
{
    public record DeleteTicketTypeRequest(int id) : IRequest<Unit>;
}
