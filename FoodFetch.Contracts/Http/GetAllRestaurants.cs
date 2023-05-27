using System.Collections.Generic;

using FoodFetch.Contracts.Models;

namespace FoodFetch.Contracts.Http
{
    public class GetRestaurantsResponse
    {
        public List<Restaurant> Restaurants { get; set; }
    }
}