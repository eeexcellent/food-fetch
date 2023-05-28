using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Enums;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.DbContexts;
using FoodFetch.Domain.Repositories;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FoodFetch.Domain.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderResult>
    {
        public IEnumerable<int> ProductList { get; set; }
        public string ClientId { get; set; }
        public string DeliveryPlace { get; set; }
        public string ClientRequest { get; set; }
    }

    public class CreateOrderResult
    {
        public string OrderId { get; set; }
        public string ClientEmail { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }
    }

    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IRepository _repository;
        private readonly FoodFetchContext _dbContext;

        public CreateOrderCommandHandler(IRepository repository, FoodFetchContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            DatabaseUser user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == request.ClientId, cancellationToken);
            if (user == null)
            {
                return new();
            }

            double totalPrice = 0;
            foreach (int productId in request.ProductList)
            {
                DatabaseProduct product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return new CreateOrderResult
                    {
                        ClientEmail = user.Email
                    };
                }
                totalPrice += product.Price;
            }

            DatabaseOrder order = new()
            {
                Id = Guid.NewGuid(),
                Price = totalPrice,
                Status = OrderStatus.Preparing,
                OrderedAt = DateTime.UtcNow,
                Request = request.ClientRequest,
                DeliveryPlace = request.DeliveryPlace,
                UserId = new Guid(request.ClientId)
            };

            List<OrderProduct> orderProducts = new(request.ProductList.Count());
            foreach (int productId in request.ProductList)
            {
                OrderProduct orderProduct = new()
                {
                    ProductId = productId,
                    OrderId = order.Id
                };
                orderProducts.Add(orderProduct);
            }

            _ = await _repository.CreateOrder(order, orderProducts, CancellationToken.None);

            return new CreateOrderResult
            {
                OrderId = order.Id.ToString(),
                ClientEmail = order.User.Email,
                TotalPrice = order.Price,
                OrderedAt = order.OrderedAt
            };
        }
    }
}