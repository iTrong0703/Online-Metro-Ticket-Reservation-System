using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketType.Queries.GetTicketTypeDetails
{
    public class TicketTypeDetailsDto
    {
        public string TicketName { get; set; }
        public int TicketPrice { get; set; }
        public int ValidityDays { get; set; }
        public bool IsStudentOnly { get; set; }
        public bool IsTimeBased { get; set; }
    }
}
