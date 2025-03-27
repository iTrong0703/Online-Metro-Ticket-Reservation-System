using AutoMapper;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Stations.Queries.GetStationDetails
{
    class GetStationDetailsRequestHandler : IRequestHandler<GetStationDetailsRequest, StationDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStationDetailsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<StationDetailsDto> Handle(GetStationDetailsRequest request, CancellationToken cancellationToken)
        {
            var station = await _unitOfWork.Stations.GetByIdAsync(request.stationId, cancellationToken);
            if (station == null)
            {
                throw new NotFoundException(nameof(station), request.stationId.ToString());
            }    
            var result = _mapper.Map<StationDetailsDto>(station);
            return result;
        }
    }
}
