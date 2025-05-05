using AutoMapper;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Application.Features.Stations.Commands.CreateStation;
using MetroTicketReservation.Application.Features.Stations.Commands.UpdateStation;
using MetroTicketReservation.Application.Features.Stations.Queries.GetAllStations;
using MetroTicketReservation.Application.Features.Stations.Queries.GetStationDetails;
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
            CreateMap<StationsDto, Station>().ReverseMap(); // both way
            CreateMap<CreateStationRequest, Station>();
            CreateMap<UpdateStationRequest, Station>();
            CreateMap<StationDetailsDto, Station>().ReverseMap();

            // map cho class có <T>
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}
