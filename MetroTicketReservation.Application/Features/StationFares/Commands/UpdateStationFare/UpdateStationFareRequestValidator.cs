using FluentValidation;
using MetroTicketReservation.Application.Features.StationFares.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.UpdateStationFare
{
    public class UpdateStationFareRequestValidator : AbstractValidator<UpdateStationFareRequest>
    {
        public UpdateStationFareRequestValidator()
        {
            RuleFor(s => s.StationFareID)
                .GreaterThanOrEqualTo(1).WithMessage("StationFareID must be a valid StationFare ID.");
            Include(new BaseStationFareRequestValidator());
        }
    }
}
