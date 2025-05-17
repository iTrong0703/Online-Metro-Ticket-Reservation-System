using MetroTicketReservation.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
        public string OrderId { get; set; } = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}{Random.Shared.Next(1000, 9999)}";
        public int Amount { get; set; }
        public string? OrderInfo { get; set; }
        public string PaymentMethod { get; set; } // momo, vnpay,...
        public PaymentStatus PaymentStatus { get; set; } // Pending, Completed, Canceled, Failed
        public DateTime? PaymentDate { get; set; }
        public int PassengerId { get; set; }
        public int? TicketId { get; set; }

        // Navigation properties
        public Passenger? Passenger { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
