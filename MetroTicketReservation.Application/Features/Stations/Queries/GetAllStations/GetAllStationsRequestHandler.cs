using AutoMapper;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;

public class GetAllStationsRequestHandler : IRequestHandler<GetAllStationsRequest, PagedResult<StationsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllStationsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<StationsDto>> Handle(GetAllStationsRequest request, CancellationToken cancellationToken)
    {
        var stations = await _unitOfWork.StationRepository.GetStationPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var result = _mapper.Map<PagedResult<StationsDto>>(stations);
        return result;
    }
}