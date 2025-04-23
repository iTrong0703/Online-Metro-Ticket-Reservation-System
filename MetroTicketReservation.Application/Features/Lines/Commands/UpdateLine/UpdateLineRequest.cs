using MediatR;
using MetroTicketReservation.Application.Features.Lines.Commands.SharedResources;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine
{
    public class UpdateLineRequest : BaseLineRequest, IRequest<Unit>
    {
        public int LineID { get; set; }
    }
}
