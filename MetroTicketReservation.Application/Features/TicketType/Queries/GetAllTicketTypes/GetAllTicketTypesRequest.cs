using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketType.Queries.GetAllTicketTypes
{
    public record GetAllTicketTypesRequest : IRequest<List<TicketTypeDto>>;
}
