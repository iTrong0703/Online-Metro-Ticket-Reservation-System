using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.SharedResources
{
    internal class BaseLineRequestValidator : AbstractValidator<BaseLineRequest>
    {
        public BaseLineRequestValidator()
        {
            RuleFor(l => l.LineName)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}
