using MetroTicketReservation.Application.Common.Interfaces.Repositories;
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
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Ticket?> GetTicketWithDetailsAsync(int ticketId)
        {
            return await _dbSet.Include(t => t.Passenger)
                .Include(t => t.StartStation)
                .Include(t => t.EndStation)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(t => t.TicketID == ticketId);
        }
    }
}
