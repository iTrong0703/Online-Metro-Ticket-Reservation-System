using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.PassengerId)
            .GreaterThan(0).WithMessage("PassengerId must be greater than 0.")
            .MustAsync(PassengerExists).WithMessage("Passenger does not exist.");

            RuleFor(x => x.TicketTypeId)
                .GreaterThan(0).WithMessage("TicketTypeId must be greater than 0.");

            RuleFor(x => x.StartStationId)
                .GreaterThan(0).When(x => x.StartStationId.HasValue)
                .WithMessage("StartStationId must be greater than 0.");

            RuleFor(x => x.EndStationId)
                .GreaterThan(0).When(x => x.EndStationId.HasValue)
                .WithMessage("EndStationId must be greater than 0.");
        }

        private async Task<bool> PassengerExists(int passengerId, CancellationToken token)
        {
            var passenger = await _unitOfWork.Passengers.GetByIdAsync(passengerId);
            return passenger != null;
        }
    }
}
