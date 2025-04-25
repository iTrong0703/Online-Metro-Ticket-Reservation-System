using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.AddStationToLine
{
    public class AddStationToLineRequestHandler : IRequestHandler<AddStationToLineRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddStationToLineRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddStationToLineRequest request, CancellationToken cancellationToken)
        {
            var validator = new AddStationToLineRequestValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var stationLine = new StationLine
            {
                StationID = request.StationID,
                LineID = request.LineID,
                StationOrder = request.StationOrder
            };
            await _unitOfWork.StationLines.AddAsync(stationLine, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
