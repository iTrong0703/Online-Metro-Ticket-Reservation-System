using MediatR;
using MetroTicketReservation.Application.Features.StationsLines.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.AddStationToLine
{
    public class AddStationToLineRequest : BaseStationLineRequest, IRequest<Unit>
    {
        public int StationOrder { get; set; }
    }
}
