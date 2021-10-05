using SparePartsOrders.DAL.Entities;
using SparePartsOrders.Models.RequestModels;
using SparePartsOrders.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.BLL.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetOrderListAsync(RequestParameters parameters);
        Task<OrderModel> GetOrderByIdAsync(string id);
        Task<OrderModel> UpdateOrderAsync(string id, OrderForUpdateModel updateOrder);
        Task RemoveOrderAsync(string id);
    }
}
