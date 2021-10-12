using AutoMapper;
using SparePartsOrders.BLL.Contracts;
using SparePartsOrders.DAL.Contracts;
using SparePartsOrders.DAL.Entities;
using SparePartsOrders.Models.RequestModels;
using SparePartsOrders.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderModel>> GetOrderListAsync(RequestParameters parameters)
        {
            var orders = await _orderRepository.GetOrderListAsync(parameters);

            return _mapper.Map<List<OrderModel>>(orders);
        }
        
        public async Task<List<OrderModel>> GetOrdetListForUserAsync(Guid userId, RequestParameters parameters)
        {
            var orders = await _orderRepository.GetOrderListForUserAsync(userId, parameters);

            return _mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<OrderModel> GetOrderByIdAsync(string id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            return _mapper.Map<OrderModel>(order);
        }

        public async Task RemoveOrderAsync(string id) =>
            await _orderRepository.RemoveOrderAsync(id);

        public async Task<OrderModel> UpdateOrderAsync(string id, OrderForUpdateModel updateOrder)
        {
            var orderEntity = _mapper.Map<Order>(updateOrder);
            orderEntity.Id = id;
            await _orderRepository.UpdateOrderAsync(id, orderEntity);

            return _mapper.Map<OrderModel>(orderEntity);
        }
    }
}
