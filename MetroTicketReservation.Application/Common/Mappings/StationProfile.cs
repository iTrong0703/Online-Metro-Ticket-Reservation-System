using AutoMapper;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Mappings
{
    public class StationProfile : Profile
    {
        public StationProfile()
        {
            CreateMap<Station, StationDto>();
        }
    }
}
