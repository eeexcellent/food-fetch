using FoodFetch.Contracts.Models;

namespace FoodFetch.Contracts.Http
{
    public class GetUserByIdResponse
    {
        public User User { get; init; }
    }
}