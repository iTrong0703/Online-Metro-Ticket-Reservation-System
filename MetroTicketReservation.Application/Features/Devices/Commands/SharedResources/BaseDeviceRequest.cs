using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Commands.SharedResources
{
    public class BaseDeviceRequest
    {
        public string DeviceName { get; set; } = string.Empty;
        public string LocationDescription { get; set; } = string.Empty;
        public int StationID { get; set; }
    }
}
