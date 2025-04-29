using FluentValidation;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.SharedResources
{
    internal class BaseTicketTypeRequestValidator : AbstractValidator<BaseTicketTypeRequest>
    {
        public BaseTicketTypeRequestValidator()
        {
            RuleFor(t => t.TicketName)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(t => t.TicketPrice)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be a non-negative value.");

            RuleFor(t => t.ValidityDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
