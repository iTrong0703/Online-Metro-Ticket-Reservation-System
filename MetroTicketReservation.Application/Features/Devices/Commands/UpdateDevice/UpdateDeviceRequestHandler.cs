using FluentValidation;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.StationFares.Commands.UpdateStationFare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceRequestHandler : IRequestHandler<UpdateDeviceRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeviceRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateDeviceRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDeviceRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var device = await _unitOfWork.Devices.GetByIdAsync(request.DeviceID);
            if (device == null)
            {
                throw new NotFoundException(nameof(device), request.DeviceID.ToString());
            }
            device.DeviceName = request.DeviceName;
            device.LocationDescription = request.LocationDescription;
            device.StationID = request.StationID;
            await _unitOfWork.Devices.UpdateAsync(device, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
