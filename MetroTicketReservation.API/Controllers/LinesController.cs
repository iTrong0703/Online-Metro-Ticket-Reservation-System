using MediatR;
using MetroTicketReservation.Application.Features.Lines.Commands.CreateLine;
using MetroTicketReservation.Application.Features.Lines.Commands.DeleteLine;
using MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine;
using MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines;
using MetroTicketReservation.Application.Features.Lines.Queries.GetLineDetails;
using MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;
using MetroTicketReservation.Application.Features.Stations.Commands.DeleteStation;
using MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Application.Features.Stations.Queries.GetStationDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LinesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllLinesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetLineDetailsRequest(id));
            return Ok(result);
        }
        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLineRequest request)
        {
            var lineId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Create), new { id = lineId }, lineId);
        }
        // DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var lineId = await _mediator.Send(new DeleteLineRequest(id));
            return NoContent();
        }
        // PUT
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateLineRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
