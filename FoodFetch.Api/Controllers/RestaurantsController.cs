using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Http;
using FoodFetch.Contracts.Models;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace FoodFetch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get the list of all restaurants
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of all the restaurants</returns>
        /// <response code="200">Restaurants returned successfully</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetRestaurantsResponse), 200)]
        public async Task<IActionResult> GetRestaurants(CancellationToken cancellationToken = default)
        {
            GetRestaurantsQuery query = new();
            GetRestaurantsResult result = await _mediator.Send(query, cancellationToken);
            return Ok(new GetRestaurantsResponse
            {
                Restaurants = result.Restaurants
            });
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetRestaurantMenu([FromRoute] int restaurantId,
            CancellationToken cancellationToken = default)
        {
            GetRestaurantMenuByIdQuery query = new()
            {
                Id = restaurantId
            };
            GetRestaurantMenuByIdResult result = await _mediator.Send(query, cancellationToken);

            if (result.Menu == null)
            {
                return NotFound(new ErrorModel
                {
                    Message = $"Restaurant with id {restaurantId} doesn't exist"
                });
            }

            List<Product> menu = new(result.Menu.Count());
            foreach (DatabaseProduct product in result.Menu)
            {
                menu.Add(new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Calories = product.Calories
                });
            }
            return Ok(new GetRestaurantMenuByIdResponse
            {
                RestaurantId = result.RestaurantId,
                RestaurantTitle = result.RestaurantTitle,
                Menu = menu
            });
        }
    }
}