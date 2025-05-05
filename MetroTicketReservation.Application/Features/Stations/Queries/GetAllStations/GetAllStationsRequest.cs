using MediatR;
using MetroTicketReservation.Application.Common.Models;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;

public class GetAllStationsRequest : PagedRequest, IRequest<PagedResult<StationsDto>> { }