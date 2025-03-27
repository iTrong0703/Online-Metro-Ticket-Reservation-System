using MediatR;

namespace MetroTicketReservation.Application.Features.Stations.Commands.DeleteStation;

public record DeleteStationRequest(int Id) : IRequest<Unit>;