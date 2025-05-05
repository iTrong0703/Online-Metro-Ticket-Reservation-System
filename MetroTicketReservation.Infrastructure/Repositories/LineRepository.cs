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
    public class LineRepository : GenericRepository<Line>, ILineRepository
    {
        public LineRepository(AppDbContext context) : base(context) {}

        public async Task<PagedResult<Line>> GetLinePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var lines = _dbSet.AsNoTracking();
            var totalCount = await lines.CountAsync();
            var items = await lines.OrderBy(l => l.LineID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<Line>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        // ...
    }
}
