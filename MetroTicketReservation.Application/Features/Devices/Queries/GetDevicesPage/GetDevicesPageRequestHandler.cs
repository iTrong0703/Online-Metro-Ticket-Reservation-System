using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Models;

namespace MetroTicketReservation.Application.Features.Devices.Queries.GetDevicesPage
{
    public class GetDevicesPageRequestHandler : IRequestHandler<GetDevicesPageRequest, PagedResult<DeviceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDevicesPageRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<DeviceDto>> Handle(GetDevicesPageRequest request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.DeviceRepository.GetDevicePagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var result = new PagedResult<DeviceDto>
            {
                Items = device.Items.Select(s => new DeviceDto
                {
                    DeviceName = s.DeviceName,
                    LocationDescription = s.LocationDescription,
                    StationName = s.Station.StationName
                }).ToList(),
                TotalCount = device.TotalCount
            };
            return result;
        }
    }
}
