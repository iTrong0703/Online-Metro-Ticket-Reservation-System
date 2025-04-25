using MediatR;
using MetroTicketReservation.Application.Features.StationsLines.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.DeleteStationFromLine
{
    public class DeleteStationFromLineRequest : BaseStationLineRequest, IRequest<Unit>
    {
    }
}
