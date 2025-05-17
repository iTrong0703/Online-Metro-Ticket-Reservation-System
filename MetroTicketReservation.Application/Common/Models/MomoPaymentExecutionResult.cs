using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Models
{
    public class MomoPaymentExecutionResult
    {
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string FullName { get; set; }
        public string OrderInfo { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
