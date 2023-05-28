using System;
using System.Collections.Generic;

namespace FoodFetch.Contracts.Http
{
    public class CreateOrderRequest
    {
        public IEnumerable<int> ProductList { get; set; }
        public string ClientId { get; set; }
        public string DeliveryPlace { get; set; }
        public string ClientRequest { get; set; }
    }
    public class CreateOrderResponse
    {
        public string OrderId { get; set; }
        public string ClientEmail { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }

    }
}