using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationRequestHandler : IRequestHandler<CreateStationRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStationRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateStationRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateStationRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var station = new Station()
            {
                StationName = request.StationName,
                Description = request.Description
            };
            await _unitOfWork.Stations.AddAsync(station);
            await _unitOfWork.SaveAllAsync();
            return station.StationID;
        }
    }
}
    