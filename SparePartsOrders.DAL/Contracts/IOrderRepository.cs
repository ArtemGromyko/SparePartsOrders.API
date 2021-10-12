using SparePartsOrders.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.DAL.Contracts
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrderListAsync(RequestParameters parameters);
        Task<List<Order>> GetOrderListForUserAsync(Guid userId, RequestParameters parameters);
        Task<Order> GetOrderByIdAsync(string id);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(string id, Order updateOrder);
        Task RemoveOrderAsync(Order removeOrder);
        Task RemoveOrderAsync(string id);
    }
}
