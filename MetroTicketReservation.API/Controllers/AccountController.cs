using MediatR;
using MetroTicketReservation.Application.Features.Passengers.Commands.LoginWithGoogle;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("google-login")]
        public async Task<ActionResult<LoginWithGoogleResponse>> GoogleLogin([FromBody] LoginWithGoogleRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
