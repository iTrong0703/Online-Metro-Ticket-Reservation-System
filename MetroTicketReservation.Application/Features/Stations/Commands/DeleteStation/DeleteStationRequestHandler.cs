using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;

namespace MetroTicketReservation.Application.Features.Stations.Commands.DeleteStation;

public class DeleteStationRequestHandler : IRequestHandler<DeleteStationRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStationRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(DeleteStationRequest request, CancellationToken cancellationToken)
    {
        var station = await _unitOfWork.Stations.GetByIdAsync(request.Id, cancellationToken);
        if (station == null)
        {
            throw new NotFoundException(nameof(station), request.Id.ToString());
        }
        await _unitOfWork.Stations.DeleteAsync(station, cancellationToken);
        await _unitOfWork.SaveAllAsync(cancellationToken);
        return Unit.Value;
    }
}