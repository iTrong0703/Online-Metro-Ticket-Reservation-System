using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.DeleteStationFare
{
    public class DeleteStationFareRequestHandler : IRequestHandler<DeleteStationFareRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStationFareRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteStationFareRequest request, CancellationToken cancellationToken)
        {
            var stationFare = await _unitOfWork.StationFares.GetByIdAsync(request.id);
            if (stationFare == null)
            {
                throw new NotFoundException(nameof(stationFare), request.id.ToString());
            }
            await _unitOfWork.StationFares.DeleteAsync(stationFare, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
