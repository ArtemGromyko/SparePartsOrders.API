using MongoDB.Driver;
using SparePartsOrders.DAL.Contracts;
using SparePartsOrders.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.CollectionName);
        }

        public async Task<List<Order>> GetOrderListAsync(RequestParameters parameters)
        {
            if (parameters.Received == null)
            {
                return await _orders.AsQueryable().ToListAsync();
            }

            var orders = await _orders.FindAsync(order => order.Received.Equals(parameters.Received));

            return await orders.ToListAsync();
        }

        public async Task<List<Order>> GetOrderListForUserAsync(Guid userId, RequestParameters parameters)
        {
            if (parameters.Received == null)
            {
                var orders =  await _orders.FindAsync(order => order.UserId.Equals(userId.ToString()));

                return await orders.ToListAsync();
            }
            else
            {
                var orders = await _orders.FindAsync(order => order.Received.Equals(parameters.Received) && order.UserId.Equals(userId.ToString()));

                return await orders.ToListAsync();
            }
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var orders = await _orders.FindAsync(order => order.Id.Equals(id));

            return await orders.FirstOrDefaultAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orders.InsertOneAsync(order);

            return order;
        }

        public async Task UpdateOrderAsync(string id, Order updateOrder) =>
            await _orders.ReplaceOneAsync(order => order.Id.Equals(id), updateOrder);

        public async Task RemoveOrderAsync(Order removeOrder) =>
            await _orders.DeleteOneAsync(order => order.Id.Equals(removeOrder.Id));

        public async Task RemoveOrderAsync(string id) =>
            await _orders.DeleteOneAsync(order => order.Id.Equals(id));
    }
}
