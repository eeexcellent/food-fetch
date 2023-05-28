using System.Collections.Generic;
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
        Task<DatabaseUser> GetUserByEmail(string email, CancellationToken cancellationToken = default);
        Task<DatabaseUser> GetUserById(string id, CancellationToken cancellationToken = default);
        Task UpdateUserById(DatabaseUser user, CancellationToken cancellationToken = default);
        Task<List<DatabaseRestaurant>> GetRestaurants(CancellationToken cancellationToken = default);
        Task<DatabaseRestaurant> GetRestaurantById(int id, CancellationToken cancellationToken = default);
        Task<DatabaseOrder> CreateOrder(
            DatabaseOrder order,
            IEnumerable<OrderProduct> orderProducts,
            CancellationToken cancellationToken = default);
        Task<DatabaseOrder> GetOrderById(string id, CancellationToken cancellationToken = default);
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

        public Task<DatabaseUser> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task UpdateUserById(DatabaseUser user, CancellationToken cancellationToken = default)
        {
            DatabaseUser userToUpdate = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.SecondName = user.SecondName;
            userToUpdate.Email = user.Email;

            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<DatabaseRestaurant>> GetRestaurants(CancellationToken cancellationToken)
        {
            return await _dbContext.Restaurants.ToListAsync(cancellationToken);
        }

        public async Task<DatabaseRestaurant> GetRestaurantById(int id, CancellationToken cancellationToken = default)
        {
            DatabaseRestaurant restaurant = await _dbContext.Restaurants.Include(x => x.Products).SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
            return restaurant;
        }

        public async Task<DatabaseOrder> CreateOrder(DatabaseOrder order, CancellationToken cancellationToken = default)
        {
            _ = await _dbContext.Orders.AddAsync(order, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }

        public async Task<DatabaseOrder> CreateOrder(DatabaseOrder order,
            IEnumerable<OrderProduct> orderProducts,
            CancellationToken cancellationToken = default)
        {
            _ = await _dbContext.Orders.AddAsync(order, cancellationToken);

            foreach (OrderProduct orderProduct in orderProducts)
            {
                _ = await _dbContext.OrderProducts.AddAsync(orderProduct, cancellationToken);
            }

            _ = await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }

        public Task<DatabaseUser> GetUserById(string id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.SingleOrDefaultAsync(u => u.Id.ToString() == id, cancellationToken);
        }

        public Task<DatabaseOrder> GetOrderById(string id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Orders.Include(x => x.Products).Include(x => x.User).FirstOrDefaultAsync(o => o.Id.ToString() == id, cancellationToken);
        }
    }
}