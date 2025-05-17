using MediatR;
using MetroTicketReservation.Application.Features.Payments.Commands.CreatePayment;
using MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPayment;
using MetroTicketReservation.Application.Features.Payments.Commands.ProcessMomoPaymentCallback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroTicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var paymentId = await _mediator.Send(request);

            return Redirect($"/api/payments/redirect/{request.PaymentMethod}?paymentId={paymentId}");
        }

        [HttpGet("redirect/{paymentMethod}")]
        public async Task<IActionResult> RedirectToPaymentGateway(string paymentMethod, [FromQuery] int paymentId)
        {
            if (string.IsNullOrEmpty(paymentMethod))
            {
                return BadRequest("Payment method is required");
            }

            switch (paymentMethod.ToLower())
            {
                case "momo":
                    var momormomoRequest = new ProcessMomoPaymentRequest { PaymentId = paymentId };
                    var paymentUrl = await _mediator.Send(momormomoRequest);
                    return Ok(new { paymentUrl });
                    //return Redirect(paymentUrl);
                case "vnpay":
                    return BadRequest("VNPay payment is not implemented yet");
                default:
                    return BadRequest($"Unsupported payment method: {paymentMethod}");
            }
        }

        [HttpGet("callback/momo")]
        public async Task<IActionResult> MomoCallback()
        {
            var callbackRequest = new ProcessMomoCallbackRequest
            {
                QueryCollection = HttpContext.Request.Query
            };

            var result = await _mediator.Send(callbackRequest);
            if (result == null)
            {
                return BadRequest("Thanh toán thất bại");
            }    

            return Ok(result);
        }

        [HttpPost("notify/momo")]
        public async Task<IActionResult> MomoNotify()
        {
            var callbackRequest = new ProcessMomoCallbackRequest
            {
                QueryCollection = HttpContext.Request.Query
            };
            var success = await _mediator.Send(callbackRequest);
            return Ok(new { Success = success });
        }
    }
}
