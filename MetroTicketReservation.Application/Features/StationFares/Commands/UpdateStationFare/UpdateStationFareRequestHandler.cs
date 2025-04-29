using FluentValidation;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.UpdateStationFare
{
    public class UpdateStationFareRequestHandler : IRequestHandler<UpdateStationFareRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStationFareRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateStationFareRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStationFareRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var stationFare = await _unitOfWork.StationFares.GetByIdAsync(request.StationFareID);
            if (stationFare == null)
            {
                throw new NotFoundException(nameof(stationFare), request.StationFareID.ToString());
            }
            stationFare.StartStationID = request.StartStationID;
            stationFare.EndStationID = request.EndStationID;
            stationFare.TicketTypeID = request.TicketTypeID;
            stationFare.Fare = request.Fare;
            await _unitOfWork.StationFares.UpdateAsync(stationFare, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
