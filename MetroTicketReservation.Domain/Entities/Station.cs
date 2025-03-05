﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class Station
    {
        public int StationID { get; set; }
        public string StationName { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation property
        public HashSet<StationLine> StationLines { get; set; } = new();
    }
}
