using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.Devices.Queries.GetDeviceDetails
{
    public class DeviceDetailsDto
    {
        public string DeviceName { get; set; }
        public string LocationDescription { get; set; }
        public string StationName { get; set; }
    }
}
