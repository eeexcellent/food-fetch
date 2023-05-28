using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Http;
using FoodFetch.Domain.Commands;
using FoodFetch.Domain.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace FoodFetch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates order
        /// </summary>
        /// <param name="request">Order information</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Addition information about order</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /orders
        ///     {
        ///         "productList": [
        ///           1, 5, 7
        ///         ],     
        ///         "clientId": "123456ab-a123-1234-ab12-123a4b56cde7"
        ///         "deliveryPlace": "34 Shevchenko Boulevard, Kyiv, Ukraine",
        ///         "clientRequest": "Add more salt please"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If order created successfully</response>
        /// <response code="404">If user ID is invalid</response>
        /// <response code="400">If some product ID is invalid or there's no products at all</response>
        [HttpPut]
        [ProducesResponseType(typeof(CreateOrderResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null || !request.ProductList.Any())
            {
                return BadRequest(new ErrorModel
                {
                    Message = "There is no products in your order"
                });
            }
            CreateOrderCommand command = new()
            {
                ProductList = request.ProductList,
                ClientId = request.ClientId,
                DeliveryPlace = request.DeliveryPlace,
                ClientRequest = request.ClientRequest
            };

            CreateOrderResult result = await _mediator.Send(command, cancellationToken);

            return result.ClientEmail == null
                ? NotFound(new ErrorModel
                {
                    Message = $"User with {request.ClientId} not found"
                })
                : result.OrderId == null
                ? BadRequest(new ErrorModel
                {
                    Message = $"Some products that you listed do not exist"
                })
                : Created($"orders/{result.OrderId}", new CreateOrderResponse
                {
                    OrderId = result.OrderId,
                    ClientEmail = result.ClientEmail,
                    TotalPrice = result.TotalPrice,
                    OrderedAt = result.OrderedAt
                });
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Order information</returns>
        /// <response code="200">Returns order information</response>
        /// <response code="404">Order not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOrderByIdResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<IActionResult> GetOrderById(string id,
            CancellationToken cancellationToken = default)
        {
            GetOrderByIdQuery query = new()
            {
                Id = id
            };

            GetOrderByIdResult result = await _mediator.Send(query, cancellationToken);

            return result.Order == null
                ? NotFound(new ErrorModel
                {
                    Message = $"Order with id {id} not found"
                })
                : Ok(new GetOrderByIdResponse()
                {
                    Order = result.Order
                });
        }
    }
}