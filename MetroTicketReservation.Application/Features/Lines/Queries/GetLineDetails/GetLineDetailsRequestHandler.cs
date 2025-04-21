using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Queries.GetLineDetails
{
    public class GetLineDetailsRequestHandler : IRequestHandler<GetLineDetailsRequest, LineDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLineDetailsRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LineDetailsDto> Handle(GetLineDetailsRequest request, CancellationToken cancellationToken)
        {
            var line = await _unitOfWork.Lines.GetByIdAsync(request.lineId, cancellationToken);
            if (line == null)
            {
                throw new NotFoundException(nameof(line), request.lineId.ToString());
            }
            var result = new LineDetailsDto
            {
                LineName = line.LineName,
                Description = line.Description
            };
            return result;
        }
    }
}
