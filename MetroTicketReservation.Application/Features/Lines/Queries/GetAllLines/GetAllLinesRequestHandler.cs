using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Queries.GetAllLines
{
    public class GetAllLinesRequestHandler : IRequestHandler<GetAllLinesRequest, PagedResult<LinesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLinesRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResult<LinesDto>> Handle(GetAllLinesRequest request, CancellationToken cancellationToken)
        {
            var lines = await _unitOfWork.LineRepository.GetLinePagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var result = new PagedResult<LinesDto>
            {
                Items = lines.Items.Select(line => new LinesDto
                {
                    LineName = line.LineName,
                    Description = line.Description
                }).ToList(),
                TotalCount = lines.TotalCount
            };
            return result;
        }

    }
}
