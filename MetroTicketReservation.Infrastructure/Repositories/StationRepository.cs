using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MetroTicketReservation.Infrastructure.Repositories;

public class StationRepository : GenericRepository<Station>, IStationRepository
{
    public StationRepository(AppDbContext context) : base(context) {}
    public async Task<Station?> GetStationsByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.StationName == name);
    }
}