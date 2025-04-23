using MetroTicketReservation.Application.Common.Interfaces.Repositories;
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

        public async Task<bool> IsLineUnique(Line line)
        {
            return await _dbSet.AnyAsync(l => l.LineName == line.LineName) == false;
        }
    }
}
