using MediatR;
using MetroTicketReservation.Application.Features.StationFares.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.CreateStationFare
{
    public class CreateStationFareRequest : BaseStationFareRequest, IRequest<int>
    {
    }
}
