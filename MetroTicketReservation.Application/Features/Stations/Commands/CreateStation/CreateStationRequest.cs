using MediatR;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationRequest : BaseStationRequest, IRequest<int>
    {
    }
}
