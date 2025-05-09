using Google.Apis.Auth;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Passengers.Commands.LoginWithGoogle
{
    public class LoginWithGoogleRequestHandler : IRequestHandler<LoginWithGoogleRequest, LoginWithGoogleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public LoginWithGoogleRequestHandler(
            IUnitOfWork unitOfWork, 
            IConfiguration configuration,
            IJwtTokenGenerator tokenGenerator
            )
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<LoginWithGoogleResponse> Handle(LoginWithGoogleRequest request, CancellationToken cancellationToken)
        {
            var payload = await ValidateGoogleToken(request.IdToken);
            if (payload == null)
            {
                return new LoginWithGoogleResponse
                {
                    Success = false,
                    Message = "Invalid Google token"
                };
            }
            // Check if user exists
            var passenger = await _unitOfWork.Passengers
                .GetSingleAsync(p => p.GoogleId == payload.Subject || p.Email == payload.Email, cancellationToken);

            if (passenger == null)
            {
                // Create new passenger
                passenger = new Passenger
                {
                    GoogleId = payload.Subject,
                    FullName = payload.Name ?? payload.Email.Split('@')[0],
                    Email = payload.Email
                };

                await _unitOfWork.Passengers.AddAsync(passenger);
                await _unitOfWork.SaveAllAsync(cancellationToken);
            }
            else if (string.IsNullOrEmpty(passenger.GoogleId))
            {
                // Update existing passenger with Google ID
                passenger.GoogleId = payload.Subject;
                await _unitOfWork.SaveAllAsync(cancellationToken);
            }

            // Generate JWT token
            string token = _tokenGenerator.GenerateToken(passenger);

            return new LoginWithGoogleResponse
            {
                Success = true,
                Token = token,
                PassengerId = passenger.PassengerID,
                FullName = passenger.FullName,
                Email = passenger.Email,
                Message = "Login successful"
            };
        }

        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Authentication:Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return payload;
        }
    }
}
