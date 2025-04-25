using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.UpdateStationOrder
{
    public class UpdateStationOrderRequestHandler : IRequestHandler<UpdateStationOrderRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStationOrderRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateStationOrderRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.StationLines.GetSingleAsync(
                x => x.StationID == request.StationID && x.LineID == request.LineID, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(request), request.StationID.ToString());
            }
            var isOrderTaken = await _unitOfWork.StationLines.AnyAsync(
                x => x.LineID == request.LineID && x.StationID != request.StationID
                && x.StationOrder == request.NewStationOrder);
            if (isOrderTaken)
            {
                throw new BusinessException("Station order is already taken in this line");
            }
            entity.StationOrder = request.NewStationOrder;
            await _unitOfWork.StationLines.UpdateAsync(entity);
            await _unitOfWork.SaveAllAsync();
            return Unit.Value;
        }
    }
}
