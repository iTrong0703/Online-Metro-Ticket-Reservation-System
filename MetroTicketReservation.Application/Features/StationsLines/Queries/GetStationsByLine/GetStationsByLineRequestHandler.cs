using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Queries.GetStationsByLine
{
    public class GetStationsByLineRequestHandler : IRequestHandler<GetStationsByLineRequest, List<StationsByLineDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStationsByLineRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<StationsByLineDto>> Handle(GetStationsByLineRequest request, CancellationToken cancellationToken)
        {
            var stations = await _unitOfWork.StationLineRepository.GetStationsByLine(request.lineId);
            var filtered = stations.OrderBy(s => s.StationOrder)
                .Select(x => new StationsByLineDto
                {
                    LineName = x.Line.LineName,
                    StationName = x.Station.StationName,
                    StationOrder = x.StationOrder
                }).ToList();
            return filtered;
        }
    }
}
