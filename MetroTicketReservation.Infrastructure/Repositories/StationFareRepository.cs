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
    public class StationFareRepository : GenericRepository<StationFare>, IStationFareRepository
    {
        public StationFareRepository(AppDbContext context) : base(context)
        {
            
        }

        //public async Task<IReadOnlyList<StationFare>> GetAllStationFares(CancellationToken cancellationToken = default)
        //{
        //    return await _dbSet.Include(s => s.StartStation)
        //                    .Include(s => s.EndStation)
        //                    .ToListAsync();
        //}

        public async Task<StationFare?> GetStationFareById(int startId, int endId, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(s => s.StartStation)
                            .Include(s => s.EndStation)
                            .FirstOrDefaultAsync(s => s.StartStationID == startId && s.EndStationID == endId, cancellationToken);
        }

        public async Task<PagedResult<StationFare>> GetStationFarePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var stationFares = _dbSet.Include(s => s.StartStation)
                                    .Include(s => s.EndStation)
                                    .AsNoTracking();
            var totalCount = await stationFares.CountAsync();
            var items = await stationFares.OrderBy(t => t.StationFareID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<StationFare>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
