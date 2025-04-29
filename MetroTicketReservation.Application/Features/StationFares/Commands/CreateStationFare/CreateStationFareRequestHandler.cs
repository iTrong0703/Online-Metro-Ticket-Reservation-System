using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.Lines.Commands.CreateLine;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.CreateStationFare
{
    public class CreateStationFareRequestHandler : IRequestHandler<CreateStationFareRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStationFareRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateStationFareRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateStationFareRequestValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var stationFare = new StationFare
            {
                StartStationID = request.StartStationID,
                EndStationID = request.EndStationID,
                TicketTypeID = request.TicketTypeID,
                Fare = request.Fare
            };
            await _unitOfWork.StationFares.AddAsync(stationFare, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return stationFare.StationFareID;
        }
    }
}
