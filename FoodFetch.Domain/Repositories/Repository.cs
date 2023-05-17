using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.DbContexts;

namespace FoodFetch.Domain.Repositories
{
    internal interface IRepository
    {
        Task AddUser(User user, CancellationToken cancellationToken = default);
    }
    internal class Repository : IRepository
    {
        private readonly FoodFetchContext _dbContext;

        public Repository(FoodFetchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user, CancellationToken cancellationToken = default)
        {
            _ = await _dbContext.Users.AddAsync(user, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}