using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines
{
    public record GetAllLinesRequest : IRequest<List<LinesDto>>;
}
