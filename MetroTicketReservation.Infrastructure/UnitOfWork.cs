using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using MetroTicketReservation.Infrastructure.Repositories;

namespace MetroTicketReservation.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericRepository<Station> Stations { get; }

    public UnitOfWork(AppDbContext context, IStationRepository stations)
    {
        _context = context;
        Stations = stations;
    }

    public Task<int> SaveAllAsync() => _context.SaveChangesAsync();
    
    public void Dispose() => _context.Dispose();

}