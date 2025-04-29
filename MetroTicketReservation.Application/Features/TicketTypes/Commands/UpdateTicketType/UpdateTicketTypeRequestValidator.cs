using FluentValidation;
using MetroTicketReservation.Application.Features.TicketTypes.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.TicketTypes.Commands.UpdateTicketType
{
    public class UpdateTicketTypeRequestValidator : AbstractValidator<UpdateTicketTypeRequest>
    {
        public UpdateTicketTypeRequestValidator()
        {
            RuleFor(t => t.TicketTypeID)
            .GreaterThanOrEqualTo(1);
            Include(new BaseTicketTypeRequestValidator());
        }
    }
}
