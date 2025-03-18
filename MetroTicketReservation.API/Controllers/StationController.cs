using MediatR;
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
    }
}
