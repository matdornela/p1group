using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.OrderAggregate;

namespace API.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
        }
    }
}