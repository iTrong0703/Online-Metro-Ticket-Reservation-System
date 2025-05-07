using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.StationFares.Commands.CreateStationFare;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceRequestHandler : IRequestHandler<CreateDeviceRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeviceRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateDeviceRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateDeviceRequestValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var device = new Device
            {
                DeviceName = request.DeviceName,
                LocationDescription = request.LocationDescription,
                StationID = request.StationID
            };
            await _unitOfWork.Devices.AddAsync(device, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return device.DeviceID;
        }
    }
}
