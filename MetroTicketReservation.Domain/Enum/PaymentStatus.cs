using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Domain.Enum
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Canceled,
        Failed
    }
}
