using FluentValidation;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;

namespace MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;

public class UpdateStationRequestValidator : AbstractValidator<UpdateStationRequest>
{
    public UpdateStationRequestValidator()
    {
        RuleFor(s => s.StationID)
            .GreaterThanOrEqualTo(1);

        Include(new BaseStationRequestValidator());
    }
}