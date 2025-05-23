﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetStationDetails
{
    public record GetStationDetailsRequest(int stationId) : IRequest<StationDetailsDto>;
}
