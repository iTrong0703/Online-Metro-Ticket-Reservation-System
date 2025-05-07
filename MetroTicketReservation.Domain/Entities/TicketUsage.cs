using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class TicketUsage
    {
        public int TicketUsageID { get; set; }
        public int TicketID { get; set; }
        // time quét vé
        public DateTime ScanTime { get; set; }
        // trạm quét vé
        public int StationID { get; set; }
        // check vào ra
        public bool IsEntry { get; set; }
        public int? DeviceID { get; set; }

        // Navigation
        public Ticket Ticket { get; set; } = null!;
        public Station Station { get; set; } = null!;
        public Device? Device { get; set; }
    }
}
