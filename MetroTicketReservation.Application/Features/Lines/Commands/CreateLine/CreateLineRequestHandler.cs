using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.CreateLine
{
    public class CreateLineRequestHandler : IRequestHandler<CreateLineRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLineRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateLineRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateLineRequestValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var line = new Line
            {
                LineName = request.LineName,
                Description = request.Description
            };
            await _unitOfWork.Lines.AddAsync(line, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return line.LineID;
        }
    }
}
