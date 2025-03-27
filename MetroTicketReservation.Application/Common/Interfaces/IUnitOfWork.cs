using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using System.Threading;

namespace MetroTicketReservation.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Station> Stations { get; }
    IStationRepository StationRepository { get;  }
    Task<int> SaveAllAsync(CancellationToken cancellationToken = default);
}