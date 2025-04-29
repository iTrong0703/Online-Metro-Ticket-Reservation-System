using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.SharedResources
{
    public class BaseStationFareRequestValidator : AbstractValidator<BaseStationFareRequest>
    {
        public BaseStationFareRequestValidator()
        {
            RuleFor(f => f.StartStationID)
            .GreaterThan(0).WithMessage("{PropertyName} must be a valid station ID.");

            RuleFor(f => f.EndStationID)
                .GreaterThan(0).WithMessage("{PropertyName} must be a valid station ID.")
                .NotEqual(f => f.StartStationID).WithMessage("Start and End stations must be different.");

            RuleFor(f => f.TicketTypeID)
                .GreaterThan(0).WithMessage("{PropertyName} must be a valid ticket type ID.");

            RuleFor(f => f.Fare)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be a non-negative value.");
        }
    }
}
