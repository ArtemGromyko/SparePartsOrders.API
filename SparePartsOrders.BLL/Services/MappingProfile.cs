using AutoMapper;
using SparePartsOrders.DAL.Entities;
using SparePartsOrders.DTO;
using SparePartsOrders.Models.RequestModels;
using SparePartsOrders.Models.ResponseModels;

namespace SparePartsOrders.BLL.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderModel>();
            CreateMap<OrderForUpdateModel, Order>();
        }
    }
}
