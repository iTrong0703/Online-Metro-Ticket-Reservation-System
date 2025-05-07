using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.SharedResources
{
    internal class BaseDeviceRequestValidator : AbstractValidator<BaseDeviceRequest>
    {
        public BaseDeviceRequestValidator()
        {
            RuleFor(d => d.DeviceName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(d => d.LocationDescription)
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(d => d.StationID)
                    .GreaterThan(0).WithMessage("StationID must be greater than zero.");
        }
    }
}
