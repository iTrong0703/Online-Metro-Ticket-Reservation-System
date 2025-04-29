using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.UpdateTicketType
{
    public class UpdateTicketTypeRequestHandler : IRequestHandler<UpdateTicketTypeRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTicketTypeRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateTicketTypeRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTicketTypeRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var ticketType = await _unitOfWork.TicketTypes.GetByIdAsync(request.TicketTypeID);
            if (ticketType == null)
            {
                throw new NotFoundException(nameof(ticketType), request.TicketTypeID.ToString());
            }
            ticketType.TicketName = request.TicketName;
            ticketType.TicketPrice = request.TicketPrice;
            ticketType.ValidityDays = request.ValidityDays;
            ticketType.IsStudentOnly = request.IsStudentOnly;
            ticketType.IsTimeBased = request.IsTimeBased;
            await _unitOfWork.TicketTypes.UpdateAsync(ticketType);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
