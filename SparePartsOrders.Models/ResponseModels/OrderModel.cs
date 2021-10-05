using System;

namespace SparePartsOrders.Models.ResponseModels
{
    public class OrderModel
    {
        public string Id { get; set; }
        public Guid RequestId { get; set; }
        public string SparePartName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Received { get; set; }
    }
}
