using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Entities
{
    public class TicketType
    {
        public int TicketTypeID { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public int TicketPrice { get; set; }
        public int ValidityDays { get; set; }
        public bool IsStudentOnly { get; set; }
        public bool IsTimeBased { get; set; }
    }
}
