using FoodFetch.Contracts.Enums;

namespace FoodFetch.Contracts.Http
{
    public class CreateUserRequest
    {
        public string FirstName { get; init; }
        public string SecondName { get; init; }
        public Role Role { get; init; }
        public string Email { get; init; }
    }
    public class CreateUserResponse
    {
        public string Id { get; init; }
    }
}