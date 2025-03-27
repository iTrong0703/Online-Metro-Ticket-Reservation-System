using AutoMapper;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;

public class UpdateStationRequestHandler : IRequestHandler<UpdateStationRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStationRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateStationRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateStationRequestValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
        }
        var station = await _unitOfWork.Stations.GetByIdAsync(request.StationID);
        if (station == null)
        {
            throw new NotFoundException(nameof(station), request.StationID.ToString());
        }
        _mapper.Map(request, station);
        await _unitOfWork.Stations.UpdateAsync(station, cancellationToken);
        await _unitOfWork.SaveAllAsync(cancellationToken);
        return Unit.Value;
    }
}