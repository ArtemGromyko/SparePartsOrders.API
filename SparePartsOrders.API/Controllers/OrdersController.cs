using Microsoft.AspNetCore.Mvc;
using SparePartsOrders.API.Entities;
using SparePartsOrders.API.Models;
using SparePartsOrders.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparePartsOrders.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _ordersService;

        public OrdersController(OrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get([FromQuery]RequestParameters parameters) =>
            await _ordersService.GetAsync(parameters);

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Put(string id, Order updateOrder)
        {
            var order = await _ordersService.GetAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            await _ordersService.UpdateAsync(id, updateOrder);

            return updateOrder;
        }
    }
}
