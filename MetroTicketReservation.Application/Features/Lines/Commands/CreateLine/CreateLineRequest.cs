using MediatR;
using MetroTicketReservation.Application.Features.Lines.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.CreateLine
{
    public class CreateLineRequest : BaseLineRequest, IRequest<int>
    {
    }
}
