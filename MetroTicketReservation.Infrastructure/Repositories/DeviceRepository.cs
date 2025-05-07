using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<Device> GetDeviceById(int deviceId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(d => d.Station)
                            .FirstOrDefaultAsync(d => d.DeviceID == deviceId, cancellationToken);
        }

        public async Task<PagedResult<Device>> GetDevicePagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var devices = _dbSet.Include(d => d.Station)
                            .AsNoTracking();
            var totalCount = await devices.CountAsync();
            var items = await devices.OrderBy(d => d.DeviceID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<Device>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
