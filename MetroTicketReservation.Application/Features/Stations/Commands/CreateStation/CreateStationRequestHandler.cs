using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Application.Exceptions;
using MetroTicketReservation.Domain.Entities;

namespace MetroTicketReservation.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationRequestHandler : IRequestHandler<CreateStationRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStationRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStationRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateStationRequestValidator(_mapper, _unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Something went wrong. ", validationResult.ToDictionary());
            }
            var station = _mapper.Map<Station>(request);
            await _unitOfWork.Stations.AddAsync(station, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
            return station.StationID;
        }
    }
}
    