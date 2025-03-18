using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Common.Interfaces.Repositories;

public interface IStationRepository : IGenericRepository<Station>
{
    Task<Station> GetStationsByNameAsync(string name);
}