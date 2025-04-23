using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.DeleteLine
{
    public class DeleteLineRequestHandler : IRequestHandler<DeleteLineRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLineRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteLineRequest request, CancellationToken cancellationToken)
        {
            var line = await _unitOfWork.Lines.GetByIdAsync(request.id, cancellationToken);
            if (line == null)
            {
                throw new NotFoundException(nameof(line), request.id.ToString());
            }
            await _unitOfWork.Lines.DeleteAsync(line, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
