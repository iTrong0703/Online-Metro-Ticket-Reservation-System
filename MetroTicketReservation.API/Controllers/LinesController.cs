using MediatR;
using MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines;
using MetroTicketReservation.Application.Features.Lines.Queries.GetLineDetails;
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
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllLinesRequest());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetLineDetailsRequest(id));
            return Ok(result);
        }
    }
}
