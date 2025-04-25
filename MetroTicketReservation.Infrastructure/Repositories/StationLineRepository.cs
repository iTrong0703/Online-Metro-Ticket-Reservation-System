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
    public class StationLineRepository : GenericRepository<StationLine>, IStationLineRepository
    {
        public StationLineRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IReadOnlyList<StationLine>> GetStationsByLine(int lineId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(x => x.Station)
                .Include(x => x.Line)
                .Where(l => l.LineID == lineId)
                .ToListAsync();
        }
    }
}
