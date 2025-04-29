using MediatR;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.UpdateTicketType
{
    public class UpdateTicketTypeRequest : BaseTicketTypeRequest, IRequest<Unit>
    {
        public int TicketTypeID { get; set; }
    }
}
