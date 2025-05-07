using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.StationFares.Queries.GetStationFareDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Queries.GetDeviceDetails
{
    public class GetDeviceDetailsRequestHandler : IRequestHandler<GetDeviceDetailsRequest, DeviceDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeviceDetailsRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DeviceDetailsDto> Handle(GetDeviceDetailsRequest request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.DeviceRepository.GetDeviceById(request.deviceId, cancellationToken);
            if (device == null)
            {
                throw new NotFoundException(nameof(device), request.deviceId.ToString());
            }
            var result = new DeviceDetailsDto
            {
                DeviceName = device.DeviceName,
                LocationDescription = device.LocationDescription,
                StationName = device.Station.StationName
            };
            return result;
        }
    }
}
