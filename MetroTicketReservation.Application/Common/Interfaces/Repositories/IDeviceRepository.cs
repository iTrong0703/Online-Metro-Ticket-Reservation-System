using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Interfaces.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<Device> GetDeviceById(int deviceId, CancellationToken cancellationToken = default);
        Task<PagedResult<Device>> GetDevicePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
