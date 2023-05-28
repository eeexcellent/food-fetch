using FoodFetch.Contracts.Enums;
namespace FoodFetch.Contracts.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
    }
}