using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Models;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Queries
{
    public class GetRestaurantMenuByIdQuery : IRequest<GetRestaurantMenuByIdResult>
    {
        public int Id { get; set; }
    }

    public class GetRestaurantMenuByIdResult
    {
        public int RestaurantId { get; set; }
        public string RestaurantTitle { get; set; }
        public IEnumerable<DatabaseProduct> Menu { get; set; }
    }

    internal class GetRestaurantMenuByIdCommandHandler : IRequestHandler<GetRestaurantMenuByIdQuery, GetRestaurantMenuByIdResult>
    {
        private readonly IRepository _repository;

        public GetRestaurantMenuByIdCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetRestaurantMenuByIdResult> Handle(GetRestaurantMenuByIdQuery request, CancellationToken cancellationToken)
        {
            DatabaseRestaurant restaurant = await _repository.GetRestaurantById(request.Id, cancellationToken);
            if (restaurant == null)
            {
                return new();
            }

            return new GetRestaurantMenuByIdResult
            {
                RestaurantId = restaurant.Id,
                RestaurantTitle = restaurant.Title,
                Menu = restaurant.Products
            };
        }
    }
}