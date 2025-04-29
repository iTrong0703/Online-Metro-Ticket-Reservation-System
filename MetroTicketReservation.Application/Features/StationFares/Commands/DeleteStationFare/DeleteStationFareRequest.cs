using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationFares.Commands.DeleteStationFare
{
    public record DeleteStationFareRequest(int id) : IRequest<Unit>;
}
