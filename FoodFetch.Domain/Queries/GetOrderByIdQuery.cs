using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Models;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Queries
{
    public class GetOrderByIdQuery : IRequest<GetOrderByIdResult>
    {
        public string Id { get; set; }
    }

    public class GetOrderByIdResult
    {
        public Order Order { get; set; }
    }
    internal class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResult>
    {
        private readonly IRepository _repository;

        public GetOrderByIdQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            DatabaseOrder dbOrder = await _repository.GetOrderById(request.Id, cancellationToken);
            if (dbOrder == null)
            {
                return new GetOrderByIdResult();
            }

            List<Product> products = new();
            foreach (DatabaseProduct dbProduct in dbOrder.Products)
            {
                Product product = new()
                {
                    Id = dbProduct.Id,
                    Title = dbProduct.Title,
                    Description = dbProduct.Description,
                    Calories = dbProduct.Calories
                };
                products.Add(product);
            }

            return new GetOrderByIdResult
            {
                Order = new Order
                {
                    Id = dbOrder.Id.ToString(),
                    ClientId = dbOrder.UserId.ToString(),
                    ClientEmail = dbOrder.User.Email,
                    Status = dbOrder.Status,
                    TotalPrice = dbOrder.Price,
                    OrderedAt = dbOrder.OrderedAt,
                    ClosedAt = dbOrder.ClosedAt,
                    DeliveryPlace = dbOrder.DeliveryPlace,
                    ClientRequest = dbOrder.Request,
                    Products = products
                }
            };
        }
    }
}