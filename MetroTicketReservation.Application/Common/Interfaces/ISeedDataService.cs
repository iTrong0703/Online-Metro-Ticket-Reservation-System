﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Interfaces
{
    public interface ISeedDataService
    {
        Task SeedAsync();
    }
}
