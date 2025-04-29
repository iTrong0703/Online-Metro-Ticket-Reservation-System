using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.SharedResources
{
    public class BaseStationFareRequest
    {
        public int StartStationID { get; set; }
        public int EndStationID { get; set; }
        public int TicketTypeID { get; set; }
        public int Fare { get; set; }
    }
}
