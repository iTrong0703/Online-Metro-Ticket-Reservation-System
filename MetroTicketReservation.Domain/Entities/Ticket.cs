using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class Ticket
    {
        public int TicketID { get; set; }

        public int TicketTypeID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int? PassengerID { get; set; } // null nếu là vé giấy không gắn hành khách
        // thêm enum
        public string? PurchaseChannel { get; set; } // "App", "Card", "StationKiosk"

        public int? StartStationID { get; set; } // Dành cho vé tuyến cụ thể
        public int? EndStationID { get; set; }

        // Trạng thái sử dụng
        public bool? IsUsed { get; set; } // Cho vé lượt
        public bool IsActive { get; set; } = true;

        // Navigation
        public TicketType TicketType { get; set; } = null!;
        public Passenger? Passenger { get; set; }
        public Station? StartStation { get; set; }
        public Station? EndStation { get; set; }

        public ICollection<TicketUsage> Usages { get; set; } = new List<TicketUsage>();
    }
}
