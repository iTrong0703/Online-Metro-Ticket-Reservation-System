using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Queries.GetAllStationFares
{
    public class GetAllStationFaresRequestHandler : IRequestHandler<GetAllStationFaresRequest, PagedResult<StationFaresDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStationFaresRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResult<StationFaresDto>> Handle(GetAllStationFaresRequest request, CancellationToken cancellationToken)
        {
            var stationFares = await _unitOfWork.StationFareRepository.GetStationFarePagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var result = new PagedResult<StationFaresDto>
            {
                Items = stationFares.Items.Select(s => new StationFaresDto
                {
                    StartStationName = s.StartStation.StationName,
                    EndStationName = s.EndStation.StationName,
                    Fare = s.Fare
                }).ToList(),
                TotalCount = stationFares.TotalCount
            };
            return result;
        }
    }
}
