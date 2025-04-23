using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine
{
    public class UpdateLineRequestHandler : IRequestHandler<UpdateLineRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLineRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateLineRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLineRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var line = await _unitOfWork.Lines.GetByIdAsync(request.LineID);
            if (line == null)
            {
                throw new NotFoundException(nameof(line), request.LineID.ToString());
            }
            line.LineName = request.LineName;
            line.Description = request.Description;
            await _unitOfWork.Lines.UpdateAsync(line, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
