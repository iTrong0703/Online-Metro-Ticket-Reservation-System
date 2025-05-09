using MediatR;

namespace MetroTicketReservation.Application.Features.Passengers.Commands.LoginWithGoogle
{
    public class LoginWithGoogleRequest : IRequest<LoginWithGoogleResponse>
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
