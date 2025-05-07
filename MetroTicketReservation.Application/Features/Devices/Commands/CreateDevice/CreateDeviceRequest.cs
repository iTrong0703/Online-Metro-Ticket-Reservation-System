using MediatR;
using MetroTicketReservation.Application.Features.Devices.Commands.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceRequest : BaseDeviceRequest, IRequest<int>
    {
    }
}
