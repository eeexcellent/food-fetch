using FoodFetch.Domain.Enums;

namespace FoodFetch.Contracts.Http
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
    }
    public class CreateUserResponse
    {
        public string Id { get; set; }
    }
}