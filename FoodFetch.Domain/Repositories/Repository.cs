using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.DbContexts;

using Microsoft.EntityFrameworkCore;

namespace FoodFetch.Domain.Repositories
{
    internal interface IRepository
    {
        Task<DatabaseUser> AddUser(DatabaseUser user, CancellationToken cancellationToken = default);
        Task<DatabaseUser> GetUserById(string id, CancellationToken cancellationToken = default);
    }
    internal class Repository : IRepository
    {
        private readonly FoodFetchContext _dbContext;

        public Repository(FoodFetchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DatabaseUser> AddUser(DatabaseUser user, CancellationToken cancellationToken = default)
        {
            _ = await _dbContext.Users.AddAsync(user, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);

            return user;
        }

        public Task<DatabaseUser> GetUserById(string id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.SingleOrDefaultAsync(u => u.Id.ToString() == id, cancellationToken);
        }
    }
}