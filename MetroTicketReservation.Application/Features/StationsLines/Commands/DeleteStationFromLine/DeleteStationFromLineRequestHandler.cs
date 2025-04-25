using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.DeleteStationFromLine
{
    public class DeleteStationFromLineRequestHandler : IRequestHandler<DeleteStationFromLineRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStationFromLineRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteStationFromLineRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.StationLines.GetSingleAsync(
                x => x.StationID == request.StationID && x.LineID == request.LineID, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(request), request.StationID.ToString());
            }
            await _unitOfWork.StationLines.DeleteAsync(entity, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
