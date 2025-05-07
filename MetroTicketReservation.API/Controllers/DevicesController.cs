using MediatR;
using MetroTicketReservation.Application.Features.Devices.Commands.CreateDevice;
using MetroTicketReservation.Application.Features.Devices.Commands.UpdateDevice;
using MetroTicketReservation.Application.Features.Devices.Queries.GetDeviceDetails;
using MetroTicketReservation.Application.Features.Devices.Queries.GetDevicesPage;
using MetroTicketReservation.Application.Features.Lines.Commands.CreateLine;
using MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine;
using MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines;
using MetroTicketReservation.Application.Features.Lines.Queries.GetLineDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevicesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeviceRequest request)
        {
            var deviceId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Create), new { id = deviceId }, deviceId);
        }
        // PUT
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateDeviceRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        // GET
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDevicesPageRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetDeviceDetailsRequest(id));
            return Ok(result);
        }
    }
}
