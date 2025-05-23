﻿using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Domain.Entities;
using MetroTicketReservation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure.Repositories
{
    public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
