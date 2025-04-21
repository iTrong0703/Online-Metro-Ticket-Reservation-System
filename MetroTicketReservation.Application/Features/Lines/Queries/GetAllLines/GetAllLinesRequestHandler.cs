using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines
{
    public class GetAllLinesRequestHandler : IRequestHandler<GetAllLinesRequest, List<LinesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLinesRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<LinesDto>> Handle(GetAllLinesRequest request, CancellationToken cancellationToken)
        {
            var lines = await _unitOfWork.Lines.GetAllAsync(cancellationToken);
            var result = lines.Select(line => new LinesDto
            {
                LineName = line.LineName,
                Description = line.Description
            }).ToList();
            return result;
        }
    }
}
