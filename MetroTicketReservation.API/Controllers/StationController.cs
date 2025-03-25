using MediatR;
using MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : Controller
    {
        private readonly IMediator _mediator;

        public StationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllStationsQuery());
            return Ok(result);
        }
        
        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStationRequest request)
        {
            var stationId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Create), new { id = stationId }, stationId);
        }
    }
}
