using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Interfaces.Repositories
{
    public interface ITicketTypeRepository : IGenericRepository<TicketType>
    {
        Task<PagedResult<TicketType>> GetTicketTypePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
