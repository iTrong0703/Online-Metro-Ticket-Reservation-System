using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using System.Threading;

namespace MetroTicketReservation.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Station> Stations { get; }
    IGenericRepository<Line> Lines { get; }
    IStationRepository StationRepository { get;  }
    ILineRepository LineRepository { get;  }
    Task<int> SaveAllAsync(CancellationToken cancellationToken = default);
}