using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using System.Threading;

namespace MetroTicketReservation.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Station> Stations { get; }
    IGenericRepository<Line> Lines { get; }
    IGenericRepository<StationLine> StationLines { get; }
    IStationRepository StationRepository { get;  }
    ILineRepository LineRepository { get;  }
    IStationLineRepository StationLineRepository { get;  }
    Task<int> SaveAllAsync(CancellationToken cancellationToken = default);
}