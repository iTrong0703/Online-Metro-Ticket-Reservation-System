using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using MetroTicketReservation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace MetroTicketReservation.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;
    public IGenericRepository<Station> Stations { get; }
    public IGenericRepository<Line> Lines{ get; }
    public IGenericRepository<TicketType> TicketTypes { get; }
    public IGenericRepository<StationFare> StationFares { get; }
    public IGenericRepository<StationLine> StationLines { get; }
    public IGenericRepository<Device> Devices { get; }
    public IGenericRepository<Passenger> Passengers { get; }
    public IGenericRepository<Ticket> Tickets { get; }
    public IGenericRepository<Domain.Entities.Payment> Payments { get; }
    public IStationRepository StationRepository { get; }
    public ILineRepository LineRepository { get; }
    public IStationLineRepository StationLineRepository { get; }
    public ITicketTypeRepository TicketTypeRepository { get; }
    public IStationFareRepository StationFareRepository { get; }
    public IDeviceRepository DeviceRepository { get;  }
    public IPassengerRepository PassengerRepository { get;  }
    public IPaymentRepository PaymentRepository { get; }
    public ITicketRepository TicketRepository { get; }

    public UnitOfWork(
        AppDbContext context, 
        IStationRepository stations,
        ILineRepository lines,
        IStationLineRepository stationLine,
        ITicketTypeRepository ticketType,
        IStationFareRepository stationFare,
        IDeviceRepository device,
        IPassengerRepository passenger,
        IPaymentRepository payment,
        ITicketRepository ticket
        )
    {
        _context = context;
        Stations = stations;
        StationRepository = stations;
        Lines = lines;
        LineRepository = lines;
        StationLines = stationLine;
        StationLineRepository = stationLine;
        TicketTypes = ticketType;
        TicketTypeRepository = ticketType;
        StationFares = stationFare;
        StationFareRepository = stationFare;
        Devices = device;
        DeviceRepository = device;
        Passengers = passenger;
        PassengerRepository = passenger;
        Payments = payment;
        PaymentRepository = payment;
        Tickets = ticket;
        TicketRepository = ticket;
    }

    public Task<int> SaveAllAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    
    public void Dispose() => _context.Dispose();

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable)
    {
        if (_transaction == null)
        {
            _transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
        }
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction != null)
                await _transaction.CommitAsync();
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}