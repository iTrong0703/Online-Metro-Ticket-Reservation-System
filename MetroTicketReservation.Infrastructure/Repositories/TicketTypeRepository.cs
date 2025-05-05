using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure.Repositories
{
    public class TicketTypeRepository : GenericRepository<TicketType>, ITicketTypeRepository
    {
        public TicketTypeRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<PagedResult<TicketType>> GetTicketTypePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var ticketTypes = _dbSet.AsNoTracking();
            var totalCount = await ticketTypes.CountAsync();
            var items = await ticketTypes.OrderBy(t => t.TicketTypeID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<TicketType>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
