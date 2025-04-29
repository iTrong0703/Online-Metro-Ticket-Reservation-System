using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Features.StationFares.Commands.SharedResources;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.CreateStationFare
{
    public class CreateStationFareRequestValidator : AbstractValidator<CreateStationFareRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStationFareRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Include(new BaseStationFareRequestValidator());
            RuleFor(x => x.StartStationID)
                .MustAsync(StationExists).WithMessage("StartStation not found");

            RuleFor(x => x.EndStationID)
                .MustAsync(StationExists).WithMessage("EndStation not found");

            RuleFor(x => x.TicketTypeID)
                .MustAsync(TicketTypeExists).WithMessage("TicketType not found");
        }

        private async Task<bool> TicketTypeExists(int ticketTypeId, CancellationToken token)
        {
            var ticketType = await _unitOfWork.TicketTypes.GetByIdAsync(ticketTypeId);
            return ticketType != null;
        }

        private async Task<bool> StationExists(int stationId, CancellationToken token)
        {
            var station = await _unitOfWork.StationRepository.GetByIdAsync(stationId);
            return station != null;
        }
    }
}
