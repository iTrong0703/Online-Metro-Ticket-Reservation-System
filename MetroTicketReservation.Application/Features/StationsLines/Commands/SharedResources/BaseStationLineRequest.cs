using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Features.StationsLines.Commands.SharedResources
{
    public class BaseStationLineRequest
    {
        public int StationID { get; set; }
        public int LineID { get; set; }
    }
}
