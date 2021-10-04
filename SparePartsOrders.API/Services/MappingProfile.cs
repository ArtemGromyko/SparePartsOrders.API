using AutoMapper;
using DroneMaintenance.DTO;
using SparePartsOrders.API.Models;

namespace SparePartsOrders.API.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
