using FluentValidation;
using MetroTicketReservation.Application.Features.Devices.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceRequestValidator : AbstractValidator<UpdateDeviceRequest>
    {
        public UpdateDeviceRequestValidator()
        {
            RuleFor(d => d.DeviceID)
                .GreaterThanOrEqualTo(1).WithMessage("DeviceID must be a valid Device ID.");
            Include(new BaseDeviceRequestValidator());
        }
    }
}
