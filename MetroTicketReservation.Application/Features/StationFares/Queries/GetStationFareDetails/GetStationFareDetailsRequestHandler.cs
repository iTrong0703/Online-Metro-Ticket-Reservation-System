using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Queries.GetStationFareDetails
{
    public class GetStationFareDetailsRequestHandler : IRequestHandler<GetStationFareDetailsRequest, StationFareDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStationFareDetailsRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<StationFareDetailsDto> Handle(GetStationFareDetailsRequest request, CancellationToken cancellationToken)
        {
            var stationFare = await _unitOfWork.StationFareRepository.GetStationFareById(request.startId, request.endId);
            if (stationFare == null)
            {
                throw new NotFoundException(nameof(stationFare), request.ToString());
            }
            var result = new StationFareDetailsDto
            {
                StartStationName = stationFare.StartStation.StationName,
                EndStationName = stationFare.EndStation.StationName,
                Fare = stationFare.Fare
            };
            return result;
        }
    }
}
