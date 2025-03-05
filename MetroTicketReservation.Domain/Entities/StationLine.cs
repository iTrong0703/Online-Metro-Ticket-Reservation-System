using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class StationLine
    {
        public int StationID { get; set; } // Composite PK
        public int LineID { get; set; } // Composite  PK
        public int StationOrder { get; set; }

        // Navigation property
        public Station Station { get; set; } = null!;
        public Line Line { get; set; } = null!;
    }
}
