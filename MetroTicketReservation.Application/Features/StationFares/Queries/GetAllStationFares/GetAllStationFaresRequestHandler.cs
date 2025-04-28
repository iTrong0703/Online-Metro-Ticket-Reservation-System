using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Queries.GetAllStationFares
{
    public class GetAllStationFaresRequestHandler : IRequestHandler<GetAllStationFaresRequest, List<StationFaresDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStationFaresRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<StationFaresDto>> Handle(GetAllStationFaresRequest request, CancellationToken cancellationToken)
        {
            var stationFares = await _unitOfWork.StationFareRepository.GetAllStationFares();
            var result = stationFares.Select(s => new StationFaresDto
            {
                StartStationName = s.StartStation.StationName,
                EndStationName = s.EndStation.StationName,
                Fare = s.Fare
            }).ToList();
            return result;
        }
    }
}
