using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Interfaces.Repositories
{
    public interface IStationFareRepository : IGenericRepository<StationFare>
    {
        //Task<IReadOnlyList<StationFare>> GetAllStationFares(CancellationToken cancellationToken = default);
        Task<StationFare> GetStationFareById(int startId, int endId, CancellationToken cancellationToken = default);
        Task<PagedResult<StationFare>> GetStationFarePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
