using FoodFetch.Domain.DbContexts;
using FoodFetch.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace FoodFetch.IntegrationTests
{
    public class RepositoryTests
    {
        private readonly IRepository _repository;

        public RepositoryTests()
        {
            _repository = new Repository(CreateContext());
        }

        private static FoodFetchContext CreateContext()
        {
            DbContextOptions<FoodFetchContext> dbContextOptions = new DbContextOptionsBuilder<FoodFetchContext>()
                .UseNpgsql("Host=localhost;Database=food_fetch;Username=postgres;Password=postgres123")
                .Options;

            return new FoodFetchContext(dbContextOptions);
        }
    }
}