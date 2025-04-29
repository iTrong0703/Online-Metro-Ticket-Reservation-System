using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.DeleteTicketType
{
    public class DeleteTicketTypeRequestHandler : IRequestHandler<DeleteTicketTypeRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTicketTypeRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteTicketTypeRequest request, CancellationToken cancellationToken)
        {
            var ticketType = await _unitOfWork.TicketTypes.GetByIdAsync(request.id);
            if (ticketType == null)
            {
                throw new NotFoundException(nameof(ticketType), request.id.ToString());
            }
            await _unitOfWork.TicketTypes.DeleteAsync(ticketType);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
