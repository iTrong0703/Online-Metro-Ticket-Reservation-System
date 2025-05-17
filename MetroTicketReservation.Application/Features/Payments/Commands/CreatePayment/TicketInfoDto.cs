using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.CreatePayment
{
    public class TicketInfoDto
    {
        public int TicketTypeId { get; set; }
        public int? StartStationId { get; set; }
        public int? EndStationId { get; set; }
        public int PassengerId { get; set; }
    }
}
