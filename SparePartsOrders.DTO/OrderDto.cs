using System;

namespace SparePartsOrders.DTO
{
    public class OrderDto
    {
        public Guid RequestId { get; set; }
        public string SparePartName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Received { get; set; }
    }
}
