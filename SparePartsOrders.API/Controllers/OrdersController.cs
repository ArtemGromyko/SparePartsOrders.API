using Microsoft.AspNetCore.Mvc;
using SparePartsOrders.BLL.Contracts;
using SparePartsOrders.DAL.Entities;
using SparePartsOrders.Models.RequestModels;
using SparePartsOrders.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService ordersService)
        {
            _orderService = ordersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderModel>>> GetOrderList([FromQuery] RequestParameters parameters) =>
            await _orderService.GetOrderListAsync(parameters);

        [HttpGet("/api/users/{userId}/orders")] 
        public async Task<ActionResult<List<OrderModel>>> GetOrderListForUser(Guid userId, [FromQuery]RequestParameters parameters)
        {
            var orders = await _orderService.GetOrdetListForUserAsync(userId, parameters);

            return orders;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> Put(string id, OrderForUpdateModel updateOrder)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            var orderModel = await _orderService.UpdateOrderAsync(id, updateOrder);

            return orderModel;
        }
    }
}
