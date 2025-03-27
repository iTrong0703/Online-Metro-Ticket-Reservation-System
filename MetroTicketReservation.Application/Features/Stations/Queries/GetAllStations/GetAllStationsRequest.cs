using MediatR;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;

public record GetAllStationsRequest : IRequest<List<StationsDto>>;