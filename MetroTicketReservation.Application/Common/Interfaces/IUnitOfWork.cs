using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Station> Stations { get; }
    Task<int> SaveAllAsync();
}