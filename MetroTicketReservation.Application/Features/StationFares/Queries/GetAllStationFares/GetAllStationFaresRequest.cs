using MediatR;
using MetroTicketReservation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Queries.GetAllStationFares
{
    public class GetAllStationFaresRequest : PagedRequest, IRequest<PagedResult<StationFaresDto>> { }
}
