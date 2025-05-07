using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string LocationDescription { get; set; } = string.Empty;
        public int StationID { get; set; } // đặt ở trạm nào

        // Navigation
        public Station Station { get; set; } = null!;
        // vé nào đã quét ở máy này
        public ICollection<TicketUsage> TicketUsages { get; set; } = new List<TicketUsage>();
    }
}
