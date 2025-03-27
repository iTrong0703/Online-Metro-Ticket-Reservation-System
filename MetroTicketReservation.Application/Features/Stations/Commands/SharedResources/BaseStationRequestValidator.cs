using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Stations.Commands.SharedResources
{
    internal class BaseStationRequestValidator : AbstractValidator<BaseStationRequest>
    {
        public BaseStationRequestValidator()
        {
            RuleFor(s => s.StationName)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}
