using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Features.Devices.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceRequestValidator : AbstractValidator<CreateDeviceRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeviceRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Include(new BaseDeviceRequestValidator());
            RuleFor(d => d.StationID)
                .MustAsync(StationExists).WithMessage("Station not found");
        }
        private async Task<bool> StationExists(int stationId, CancellationToken token)
        {
            var station = await _unitOfWork.StationRepository.GetByIdAsync(stationId);
            return station != null;
        }
    }
}
