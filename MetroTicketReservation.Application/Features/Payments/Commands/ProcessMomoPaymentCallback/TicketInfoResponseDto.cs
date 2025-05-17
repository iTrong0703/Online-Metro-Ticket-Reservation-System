using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPaymentCallback
{
    public class TicketInfoResponseDto
    {
        public int TicketId { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public string TicketType { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string? StartStationName { get; set; }
        public string? EndStationName { get; set; }
    }
}
