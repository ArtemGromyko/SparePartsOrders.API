using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SparePartsOrders.API.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RequestId { get; set; }
        public string SparePartName { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool Received { get; set; }
    }
}
