using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Queries.GetStationFareDetails
{
    public record GetStationFareDetailsRequest(int startId, int endId) : IRequest<StationFareDetailsDto>;
}
