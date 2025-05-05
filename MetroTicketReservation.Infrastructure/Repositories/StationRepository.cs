using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MetroTicketReservation.Infrastructure.Repositories;

public class StationRepository : GenericRepository<Station>, IStationRepository
{
    public StationRepository(AppDbContext context) : base(context) {}

    public async Task<PagedResult<Station>> GetStationPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var stations = _dbSet.AsNoTracking();
        var totalCount = await stations.CountAsync();
        var items = await stations.OrderBy(s => s.StationID)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedResult<Station>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<bool> IsStationUnique(Station station)
    {
        return await _dbSet.AnyAsync(s => s.StationName == station.StationName) == false;
    }
}