using MediatR;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;

public record GetAllStationsQuery : IRequest<List<StationDto>>;