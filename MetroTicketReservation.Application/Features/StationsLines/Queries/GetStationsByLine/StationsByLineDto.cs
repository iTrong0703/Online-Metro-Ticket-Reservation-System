using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Queries.GetStationsByLine
{
    public class StationsByLineDto
    {
        public string LineName { get; set; }
        public string StationName { get; set; }
        public int StationOrder { get; set; }
    }
}
