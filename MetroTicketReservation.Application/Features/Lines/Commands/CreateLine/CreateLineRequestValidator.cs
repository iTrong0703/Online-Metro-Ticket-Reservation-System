using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Features.Lines.Commands.SharedResources;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.CreateLine
{
    public class CreateLineRequestValidator : AbstractValidator<CreateLineRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLineRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Include(new BaseLineRequestValidator());
            RuleFor(t => t)
                .MustAsync(CheckLineUnique).WithMessage("Line is already exits.");
        }

        private async Task<bool> CheckLineUnique(CreateLineRequest request, CancellationToken token)
        {
            var line = new Line
            {
                LineName = request.LineName,
                Description = request.Description
            };
            return !await _unitOfWork.Lines.AnyAsync(
                l => l.LineName == line.LineName);
        }
    }
}
