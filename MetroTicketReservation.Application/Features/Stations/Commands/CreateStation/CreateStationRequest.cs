using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationRequest : IRequest<int>
    {
        public string StationName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
