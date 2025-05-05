using MediatR;
using MetroTicketReservation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines
{
    public class GetAllLinesRequest : PagedRequest, IRequest<PagedResult<LinesDto>> { }
}
