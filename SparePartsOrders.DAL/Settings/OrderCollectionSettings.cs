using SparePartsOrders.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsOrders.DAL.Settings
{
    public class OrderCollectionSettings : IDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
