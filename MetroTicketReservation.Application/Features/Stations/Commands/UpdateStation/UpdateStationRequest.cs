using MediatR;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;

namespace MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;

public class UpdateStationRequest : BaseStationRequest, IRequest<Unit>
{
    public int StationID { get; set; }
}