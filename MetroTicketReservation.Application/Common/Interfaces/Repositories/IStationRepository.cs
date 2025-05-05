using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Common.Interfaces.Repositories;

public interface IStationRepository : IGenericRepository<Station>
{
    Task<bool> IsStationUnique(Station station);
    Task<PagedResult<Station>> GetStationPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}