using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Queries.GetStationFareDetails
{
    public class StationFareDetailsDto
    {
        public string StartStationName { get; set; }
        public string EndStationName { get; set; }
        public int Fare { get; set; }
    }
}
