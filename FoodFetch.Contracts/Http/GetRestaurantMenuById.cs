using System.Collections.Generic;

using FoodFetch.Contracts.Models;

namespace FoodFetch.Contracts.Http
{
    public class GetRestaurantMenuByIdResponse
    {
        public int RestaurantId { get; set; }
        public string RestaurantTitle { get; set; }
        public IEnumerable<Product> Menu { get; set; }
    }
}