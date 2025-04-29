using AutoMapper;
using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;

public class CreateStationRequestValidator : AbstractValidator<CreateStationRequest>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStationRequestValidator(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        Include(new BaseStationRequestValidator()); // AbstractValidator là abstract class nên chỉ implement đc 1, nên muốn thêm thì dùng Include

        RuleFor(t => t)
            .MustAsync(CheckStationUnique).WithMessage("Station name is already exits.");
    }

    private async Task<bool> CheckStationUnique(CreateStationRequest request, CancellationToken token)
    {
        var station = _mapper.Map<Station>(request);
        return await _unitOfWork.StationRepository.IsStationUnique(station);
    }
}