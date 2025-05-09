using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Common.Options;
using MetroTicketReservation.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(Passenger passenger)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, passenger.PassengerID.ToString()),
                new Claim(ClaimTypes.Email, passenger.Email)
            };

            if (!string.IsNullOrEmpty(passenger.GoogleId))
            {
                claims.Add(new Claim("GoogleId", passenger.GoogleId));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtOptions.ExpiryInDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
