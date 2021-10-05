using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsOrders.Models.RequestModels
{
    public class OrderForUpdateModel
    {
        public Guid RequestId { get; set; }
        public string SparePartName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Received { get; set; }
    }
}
