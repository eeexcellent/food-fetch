using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Models;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResult>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdResult
    {
        public User User { get; set; }
    }
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        private readonly IRepository _repository;

        public GetUserByIdQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            DatabaseUser dbUser = await _repository.GetUserById(request.Id, cancellationToken);
            return dbUser == null
                ? new GetUserByIdResult()
                : new GetUserByIdResult
                {
                    User = new User
                    {
                        FirstName = dbUser.FirstName,
                        SecondName = dbUser.SecondName,
                        Role = dbUser.Role,
                        Email = dbUser.Email
                    }
                };
        }
    }
}