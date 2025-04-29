using MediatR;
using MetroTicketReservation.Application.Features.Lines.Commands.DeleteLine;
using MetroTicketReservation.Application.Features.StationFares.Commands.CreateStationFare;
using MetroTicketReservation.Application.Features.StationFares.Commands.DeleteStationFare;
using MetroTicketReservation.Application.Features.StationFares.Commands.UpdateStationFare;
using MetroTicketReservation.Application.Features.StationFares.Queries.GetAllStationFares;
using MetroTicketReservation.Application.Features.StationFares.Queries.GetStationFareDetails;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.CreateTicketType;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.DeleteTicketType;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.UpdateTicketType;
using MetroTicketReservation.Application.Features.TicketTypes.Queries.GetAllTicketTypes;
using MetroTicketReservation.Application.Features.TicketTypes.Queries.GetTicketTypeDetails;
using MetroTicketReservation.Domain.Entities;
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
        /// ticket-types
        // GET
        [HttpGet("ticket-types")]
        public async Task<IActionResult> GetAllTicketTypes()
        {
            var result = await _mediator.Send(new GetAllTicketTypesRequest());
            return Ok(result);
        }
        [HttpGet("ticket-types/{id}")]
        public async Task<IActionResult> GetTicketTypeDetails(int id)
        {
            var result = await _mediator.Send(new GetTicketTypeDetailsRequest(id));
            return Ok(result);
        }
        // CREATE
        [HttpPost("ticket-types")]
        public async Task<IActionResult> CreateTicketType([FromBody] CreateTicketTypeRequest request)
        {
            var ticketTypeId = await _mediator.Send(request);
            return CreatedAtAction(nameof(CreateTicketType), new { id = ticketTypeId }, ticketTypeId);
        }
        // PUT
        [HttpPut("ticket-types")]
        public async Task<IActionResult> UpdateTicketType([FromBody] UpdateTicketTypeRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        // DELETE
        [HttpDelete("ticket-types")]
        public async Task<IActionResult> DeleteTicketType(int id)
        {
            var ticketTypeId = await _mediator.Send(new DeleteTicketTypeRequest(id));
            return NoContent();
        }

        /// station-fares
        // GET
        [HttpGet("station-fares")]
        public async Task<IActionResult> GetAllStationFare()
        {
            var result = await _mediator.Send(new GetAllStationFaresRequest());
            return Ok(result);
        }
        [HttpGet("station-fares/detail")]
        public async Task<IActionResult> GetStationFareDetails([FromQuery] int startId, [FromQuery] int endId)
        {
            var result = await _mediator.Send(new GetStationFareDetailsRequest(startId, endId));
            return Ok(result);
        }
        // CREATE
        [HttpPost("station-fares")]
        public async Task<IActionResult> CreateStationFare([FromBody] CreateStationFareRequest request)
        {
            var stationFareId = await _mediator.Send(request);
            return CreatedAtAction(nameof(CreateStationFare), new { id = stationFareId }, stationFareId);
        }
        // PUT
        [HttpPut("station-fares")]
        public async Task<IActionResult> UpdateStationFare([FromBody] UpdateStationFareRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        // DELETE
        [HttpDelete("station-fares")]
        public async Task<IActionResult> DeleteStationFare(int id)
        {
            var stationFareId = await _mediator.Send(new DeleteStationFareRequest(id));
            return NoContent();
        }
    }
}
