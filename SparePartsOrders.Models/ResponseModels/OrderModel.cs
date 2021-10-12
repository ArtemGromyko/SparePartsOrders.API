using System;

namespace SparePartsOrders.Models.ResponseModels
{
    public class OrderModel
    {
        public string Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string SparePartName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Received { get; set; }
    }
}
