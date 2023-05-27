using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Models;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Queries
{
    public class GetRestaurantsQuery : IRequest<GetRestaurantsResult>
    {
    }

    public class GetRestaurantsResult
    {
        public List<Restaurant> Restaurants { get; set; }
    }
    internal class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, GetRestaurantsResult>
    {
        private readonly IRepository _repository;

        public GetRestaurantsQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetRestaurantsResult> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            List<DatabaseRestaurant> dbRestaurants = await _repository.GetRestaurants(cancellationToken);
            List<Restaurant> restaurants = new(dbRestaurants.Count);
            foreach (DatabaseRestaurant dbRestaurant in dbRestaurants)
            {
                restaurants.Add(new Restaurant
                {
                    Id = dbRestaurant.Id,
                    Title = dbRestaurant.Title,
                    Description = dbRestaurant.Description
                });
            }

            return new GetRestaurantsResult
            {
                Restaurants = restaurants
            };
        }
    }
}