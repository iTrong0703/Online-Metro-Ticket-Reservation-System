using AutoMapper;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;

public class GetAllStationsRequestHandler : IRequestHandler<GetAllStationsRequest, List<StationsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllStationsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<StationsDto>> Handle(GetAllStationsRequest request, CancellationToken cancellationToken)
    {
        var stations = await _unitOfWork.Stations.GetAllAsync(cancellationToken);
        var result = _mapper.Map<List<StationsDto>>(stations);
        return _mapper.Map<List<StationsDto>>(stations);
    }
}