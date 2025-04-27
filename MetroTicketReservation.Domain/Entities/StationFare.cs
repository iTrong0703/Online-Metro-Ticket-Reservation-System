using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class StationFare
    {
        public int StationFareID { get; set; }
        public int StartStationID { get; set; }
        public int EndStationID { get; set; }
        public int TicketTypeID { get; set; }
        public int Fare { get; set; }

        // Navigation property
        public Station StartStation { get; set; } = null!;
        public Station EndStation { get; set; } = null!;
        public TicketType TicketType { get; set; } = null!;
    }
}
