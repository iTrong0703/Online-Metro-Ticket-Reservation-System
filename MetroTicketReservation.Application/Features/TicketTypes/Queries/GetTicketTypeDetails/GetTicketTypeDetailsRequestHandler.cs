using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Queries.GetTicketTypeDetails
{
    public class GetTicketTypeDetailsRequestHandler : IRequestHandler<GetTicketTypeDetailsRequest, TicketTypeDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTicketTypeDetailsRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TicketTypeDetailsDto> Handle(GetTicketTypeDetailsRequest request, CancellationToken cancellationToken)
        {
            var ticketType = await _unitOfWork.TicketTypes.GetByIdAsync(request.ticketTypeId);
            if (ticketType == null)
            {
                throw new NotFoundException(nameof(ticketType), request.ticketTypeId.ToString());
            }
            var result = new TicketTypeDetailsDto
            {
                TicketName = ticketType.TicketName,
                TicketPrice = ticketType.TicketPrice,
                ValidityDays = ticketType.ValidityDays,
                IsStudentOnly = ticketType.IsStudentOnly,
                IsTimeBased = ticketType.IsTimeBased
            };
            return result;
        }
    }
}
