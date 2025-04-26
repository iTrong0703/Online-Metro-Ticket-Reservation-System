using MediatR;
using MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;
using MetroTicketReservation.Application.Features.Stations.Commands.DeleteStation;
using MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Application.Features.StationsLines.Commands.AddStationToLine;
using MetroTicketReservation.Application.Features.StationsLines.Commands.DeleteStationFromLine;
using MetroTicketReservation.Application.Features.StationsLines.Commands.UpdateStationOrder;
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
        // Get
        [HttpGet("{lineId}")]
        public async Task<IActionResult> Get(int lineId)
        {
            var result = await _mediator.Send(new GetStationsByLineRequest(lineId));
            return Ok(result);
        }
        // CREATE
        [HttpPost]
        public async Task<IActionResult> AddStationToLine([FromBody] AddStationToLineRequest request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }
        // PUT
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateStationOrderRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        // DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteStationFromLineRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
