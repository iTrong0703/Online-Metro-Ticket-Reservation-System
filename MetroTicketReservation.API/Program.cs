using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Infrastructure;
using MetroTicketReservation.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);




var app = builder.Build();

//Seed data for first run
using (var scope = app.Services.CreateScope())
{
    var seedServices = scope.ServiceProvider.GetRequiredService<ISeedDataService>();
    await seedServices.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }