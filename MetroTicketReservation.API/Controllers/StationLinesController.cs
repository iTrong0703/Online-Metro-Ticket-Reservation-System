using MediatR;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Application.Features.StationsLines.Queries.GetStationsByLine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationLinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StationLinesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{lineId}/stations")]
        public async Task<IActionResult> Get(int lineId)
        {
            var result = await _mediator.Send(new GetStationsByLineRequest(lineId));
            return Ok(result);
        }
    }
}
