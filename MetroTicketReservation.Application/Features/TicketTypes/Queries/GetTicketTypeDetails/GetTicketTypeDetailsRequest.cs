using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Queries.GetTicketTypeDetails
{
    public record GetTicketTypeDetailsRequest(int ticketTypeId) : IRequest<TicketTypeDetailsDto>;
}
