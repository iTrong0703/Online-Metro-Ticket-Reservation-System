﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Stations.Commands.SharedResources
{
    public class BaseStationRequest
    {
        public string StationName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
