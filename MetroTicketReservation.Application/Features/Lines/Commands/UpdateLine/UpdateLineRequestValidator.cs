using FluentValidation;
using MetroTicketReservation.Application.Features.Lines.Commands.SharedResources;
using MetroTicketReservation.Application.Features.Stations.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Lines.Commands.UpdateLine
{
    public class UpdateLineRequestValidator : AbstractValidator<UpdateLineRequest>
    {
        public UpdateLineRequestValidator()
        {
            RuleFor(l => l.LineID)
            .GreaterThanOrEqualTo(1);

            Include(new BaseLineRequestValidator());
        }
    }
}
