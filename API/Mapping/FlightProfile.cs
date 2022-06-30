using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;

namespace API.Mapping
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight, FlightViewModel>().ReverseMap();
        }
    }
}