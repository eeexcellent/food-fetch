namespace FoodFetch.Contracts.Http
{
    public class UpdateUserByIdRequest
    {
        public string FirstName { get; init; }
        public string SecondName { get; init; }
        public string Email { get; init; }
    }
}