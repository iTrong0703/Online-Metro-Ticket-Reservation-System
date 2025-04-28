using MediatR;
using MetroTicketReservation.Application.Features.StationFares.Queries.GetAllStationFares;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Application.Features.TicketType.Queries.GetAllTicketTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet("ticket-types")]
        public async Task<IActionResult> GetAllTicketTypes()
        {
            var result = await _mediator.Send(new GetAllTicketTypesRequest());
            return Ok(result);
        }
        [HttpGet("station-fares")]
        public async Task<IActionResult> GetAllStationFare()
        {
            var result = await _mediator.Send(new GetAllStationFaresRequest());
            return Ok(result);
        }
    }
}
