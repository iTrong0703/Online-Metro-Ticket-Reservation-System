using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class Passenger
    {
        public int PassengerID { get; set; }
        public string FullName { get; set; } = string.Empty;

        // Google Identity
        public string GoogleId { get; set; } = string.Empty;
       
        public string Email { get; set; } = string.Empty;

        public string? CardUID { get; set; } // thẻ cứng

        // Navigation
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
