using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Queries.GetAllTicketTypes
{
    public class GetAllTicketTypesRequestHandler : IRequestHandler<GetAllTicketTypesRequest, List<TicketTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTicketTypesRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TicketTypeDto>> Handle(GetAllTicketTypesRequest request, CancellationToken cancellationToken)
        {
            var ticketTypes = await _unitOfWork.TicketTypes.GetAllAsync(cancellationToken);
            var result = ticketTypes.Select(t => new TicketTypeDto
            {
                TicketName = t.TicketName,
                TicketPrice = t.TicketPrice,
                ValidityDays = t.ValidityDays,
                IsStudentOnly = t.IsStudentOnly,
                IsTimeBased = t.IsTimeBased
            }).ToList();
            return result;
        }
    }
}
