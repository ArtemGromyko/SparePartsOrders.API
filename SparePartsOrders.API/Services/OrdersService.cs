using MongoDB.Driver;
using SparePartsOrders.API.Entities;
using SparePartsOrders.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.API.Services
{
    public class OrdersService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrdersService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.CollectionName);
        }

        public async Task<List<Order>> GetAsync(RequestParameters parameters)
        {
            if (parameters.Received == null)
                return await _orders.FindAsync(order => true).Result.ToListAsync();

            return await _orders.FindAsync(order => order.Received == parameters.Received).Result.ToListAsync();
        }     
        
        public async Task<Order> GetAsync(string id) =>
            await _orders.FindAsync(order => order.Id == id).Result.FirstOrDefaultAsync();

        public async Task<Order> CreateAsync(Order order)
        {
            await _orders.InsertOneAsync(order);

            return order;
        }

        public async Task UpdateAsync(string id, Order updateOrder) =>
            await _orders.ReplaceOneAsync(order => order.Id == id, updateOrder);

        public async Task RemoveAsync(Order removeOrder) =>
            await _orders.DeleteOneAsync(order => order.Id == removeOrder.Id);

        public async Task RemoveAsync(string id) =>
            await _orders.DeleteOneAsync(order => order.Id == id);
    }
}
