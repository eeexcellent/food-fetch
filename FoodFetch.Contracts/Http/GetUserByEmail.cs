using FoodFetch.Contracts.Models;

namespace FoodFetch.Contracts.Http
{
    public class GetUserByEmailResponse
    {
        public User User { get; init; }
    }
}