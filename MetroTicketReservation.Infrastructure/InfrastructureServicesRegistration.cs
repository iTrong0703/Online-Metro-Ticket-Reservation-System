using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Interfaces.Repositories;
using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Common.Options;
using MetroTicketReservation.Infrastructure.Authentication;
using MetroTicketReservation.Infrastructure.Data;
using MetroTicketReservation.Infrastructure.Repositories;
using MetroTicketReservation.Infrastructure.Services.Cache;
using MetroTicketReservation.Infrastructure.Services.Payment.Momo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"));
            });
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.Configure<MomoOptions>(configuration.GetSection("MomoAPI"));
            services.AddScoped<ISeedDataService, SeedDataService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<ILineRepository, LineRepository>();
            services.AddScoped<IStationLineRepository, StationLineRepository>();
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<IStationFareRepository, StationFareRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            // configuration login
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidAudience = configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            // cache redis
            var redis = ConnectionMultiplexer.Connect(configuration["CacheSettings:ConnectionString"]);
            services.AddSingleton<IConnectionMultiplexer>(redis);

            services.AddTransient<IMomoPaymentService, MomoPaymentService>();
            services.AddTransient<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
