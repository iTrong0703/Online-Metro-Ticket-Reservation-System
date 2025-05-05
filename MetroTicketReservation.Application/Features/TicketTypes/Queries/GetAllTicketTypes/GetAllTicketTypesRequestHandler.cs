using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Queries.GetAllTicketTypes
{
    public class GetAllTicketTypesRequestHandler : IRequestHandler<GetAllTicketTypesRequest, PagedResult<TicketTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTicketTypesRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResult<TicketTypeDto>> Handle(GetAllTicketTypesRequest request, CancellationToken cancellationToken)
        {
            var ticketTypes = await _unitOfWork.TicketTypeRepository.GetTicketTypePagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var result = new PagedResult<TicketTypeDto>
            {
                Items = ticketTypes.Items.Select(t => new TicketTypeDto
                {
                    TicketName = t.TicketName,
                    TicketPrice = t.TicketPrice,
                    ValidityDays = t.ValidityDays,
                    IsStudentOnly = t.IsStudentOnly,
                    IsTimeBased = t.IsTimeBased
                }).ToList(),
                TotalCount = ticketTypes.TotalCount
            };
            return result;
        }
    }
}
