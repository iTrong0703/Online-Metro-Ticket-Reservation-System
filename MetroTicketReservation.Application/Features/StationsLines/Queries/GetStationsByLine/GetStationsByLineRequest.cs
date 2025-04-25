using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Queries.GetStationsByLine
{
    public record GetStationsByLineRequest(int lineId) : IRequest<List<StationsByLineDto>>;
}
