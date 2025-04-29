using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.CreateTicketType
{
    public class CreateTicketTypeRequestHandler : IRequestHandler<CreateTicketTypeRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTicketTypeRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateTicketTypeRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateTicketTypeRequestValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var ticketType = new TicketType
            {
                TicketName = request.TicketName,
                TicketPrice = request.TicketPrice,
                ValidityDays = request.ValidityDays,
                IsStudentOnly = request.IsStudentOnly,
                IsTimeBased = request.IsTimeBased
            };
            await _unitOfWork.TicketTypes.AddAsync(ticketType);
            await _unitOfWork.SaveAllAsync();
            return ticketType.TicketTypeID;
        }
    }
}
