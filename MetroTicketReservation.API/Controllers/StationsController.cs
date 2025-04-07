using MediatR;
using MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;
using MetroTicketReservation.Application.Features.Stations.Commands.DeleteStation;
using MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Application.Features.Stations.Queries.GetStationDetails;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : Controller
    {
        private readonly IMediator _mediator;

        public StationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllStationsRequest());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetStationDetailsRequest(id));
            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStationRequest request)
        {
            var stationId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Create), new { id = stationId }, stationId);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateStationRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var stationId = await _mediator.Send(new DeleteStationRequest(id));
            return NoContent();
        }
    }
}
