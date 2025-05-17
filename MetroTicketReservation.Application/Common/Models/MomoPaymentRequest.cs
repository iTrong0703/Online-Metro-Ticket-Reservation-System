using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Models
{
    public class MomoPaymentRequest
    {
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public string FullName { get; set; }
        public string OrderInfo { get; set; }
    }
}
