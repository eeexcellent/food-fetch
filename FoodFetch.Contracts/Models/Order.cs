using System;
using System.Collections.Generic;

using FoodFetch.Contracts.Enums;

namespace FoodFetch.Contracts.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ClientEmail { get; set; }
        public OrderStatus Status { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string DeliveryPlace { get; set; }
        public string ClientRequest { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}