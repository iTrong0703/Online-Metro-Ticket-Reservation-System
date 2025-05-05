using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Models
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
