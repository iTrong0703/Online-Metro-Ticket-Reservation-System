using AutoMapper;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;

public class GetAllStationsQueryHandler : IRequestHandler<GetAllStationsQuery, List<StationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllStationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<StationDto>> Handle(GetAllStationsQuery request, CancellationToken cancellationToken)
    {
        var stations = await _unitOfWork.Stations.GetAllAsync();
        var result = _mapper.Map<List<StationDto>>(stations);
        return _mapper.Map<List<StationDto>>(stations);
    }
}