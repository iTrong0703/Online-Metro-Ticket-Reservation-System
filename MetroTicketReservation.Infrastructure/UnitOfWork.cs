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
    public IGenericRepository<Line> Lines{ get; }
    public IStationRepository StationRepository { get; }
    public ILineRepository LineRepository { get; }

    public UnitOfWork(
        AppDbContext context, 
        IStationRepository stations, IStationRepository stationRepository,
        ILineRepository lines, ILineRepository lineRepository
        )
    {
        _context = context;
        Stations = stations;
        StationRepository = stationRepository;
        Lines = lines;
        LineRepository = lineRepository;
    }

    public Task<int> SaveAllAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    
    public void Dispose() => _context.Dispose();

}