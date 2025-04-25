using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Interfaces.Repositories
{
    public interface IStationLineRepository : IGenericRepository<StationLine>
    {
        Task<IReadOnlyList<StationLine>> GetStationsByLine(int lineId, CancellationToken cancellationToken = default);
    }
}
