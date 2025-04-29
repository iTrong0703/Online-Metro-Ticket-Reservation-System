using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.SharedResources;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.CreateTicketType
{
    public class CreateTicketTypeRequestValidator : AbstractValidator<CreateTicketTypeRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTicketTypeRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Include(new BaseTicketTypeRequestValidator());
            RuleFor(t => t)
                .MustAsync(CheckTicketTypeUnique).WithMessage("TicketType Name is already exits.");
        }

        private async Task<bool> CheckTicketTypeUnique(CreateTicketTypeRequest request, CancellationToken token)
        {
            return !await _unitOfWork.TicketTypes.AnyAsync(
                t => t.TicketName == request.TicketName);
        }
    }
}
