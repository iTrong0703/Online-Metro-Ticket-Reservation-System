using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Queries.GetLineDetails
{
    public record GetLineDetailsRequest(int lineId) : IRequest<LineDetailsDto>;
}
