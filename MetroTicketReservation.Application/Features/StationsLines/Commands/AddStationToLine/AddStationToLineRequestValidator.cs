using FluentValidation;
using MetroTicketReservation.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.AddStationToLine
{
    public class AddStationToLineRequestValidator : AbstractValidator<AddStationToLineRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddStationToLineRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(s => s.StationID)
                .MustAsync(StationExists).WithMessage("Station does not exist");
            RuleFor(l => l.LineID)
                .MustAsync(LineExists).WithMessage("Line does not exist");
            RuleFor(x => x)
                .MustAsync(CheckStationOrderUnique).WithMessage("StationOrder already exists in this line");
            RuleFor(x => x)
                .MustAsync(CheckStationInLine).WithMessage("Station already exists in this line");
        }

        private async Task<bool> CheckStationInLine(AddStationToLineRequest request, CancellationToken token)
        {
            return !await _unitOfWork.StationLines.AnyAsync(
                x => x.LineID == request.LineID && x.StationID == request.StationID, token);
        }

        private async Task<bool> CheckStationOrderUnique(AddStationToLineRequest request, CancellationToken token)
        {
            return !await _unitOfWork.StationLines.AnyAsync(
                x => x.LineID == request.LineID && x.StationOrder == request.StationOrder, token);
        }

        private async Task<bool> StationExists(int stationId, CancellationToken token)
        {
            var station = await _unitOfWork.Stations.GetByIdAsync(stationId);
            return station != null;
        }

        private async Task<bool> LineExists(int lineId, CancellationToken token)
        {
            var line = await _unitOfWork.Lines.GetByIdAsync(lineId);
            return line != null;
        }
    }
}
