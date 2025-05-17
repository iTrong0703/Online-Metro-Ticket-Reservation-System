using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using System.Data;
using System.Threading;

namespace MetroTicketReservation.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Station> Stations { get; }
    IGenericRepository<Line> Lines { get; }
    IGenericRepository<TicketType> TicketTypes { get; }
    IGenericRepository<StationLine> StationLines { get; }
    IGenericRepository<StationFare> StationFares { get; }
    IGenericRepository<Device> Devices { get; }
    IGenericRepository<Passenger> Passengers { get; }
    IGenericRepository<Payment> Payments { get; }
    IGenericRepository<Ticket> Tickets { get; }
    IStationRepository StationRepository { get;  }
    ILineRepository LineRepository { get;  }
    IStationLineRepository StationLineRepository { get;  }
    ITicketTypeRepository TicketTypeRepository { get;  }
    IStationFareRepository StationFareRepository { get; }
    IDeviceRepository DeviceRepository { get; }
    IPassengerRepository PassengerRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    ITicketRepository TicketRepository { get;  }


    Task<int> SaveAllAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}