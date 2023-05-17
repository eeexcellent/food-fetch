using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FoodFetch.Contracts.Http;

using Microsoft.AspNetCore.Mvc.Testing;

using Newtonsoft.Json;

using Shouldly;

namespace FoodFetch.IntegrationTests
{
    public class UsersApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateUserShouldDoItSuccessfully()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.PutAsync("users", new StringContent(
                JsonConvert.SerializeObject(new CreateUserRequest
                {
                    FirstName = "testFirstName",
                    SecondName = "testSecondName",
                    Role = 0,
                    Email = Guid.NewGuid().ToString()
                }), Encoding.UTF8, "application/json"));

            // Assert
            _ = response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            CreateUserResponse responseModel = JsonConvert.DeserializeObject<CreateUserResponse>(responseString);
            responseModel.Id.ShouldNotBeEmpty();
            response.Headers.Location.ToString().ShouldBe($"users/{responseModel.Id}");
        }
    }
}