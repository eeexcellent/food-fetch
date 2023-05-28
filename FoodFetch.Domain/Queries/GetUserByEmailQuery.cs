using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Models;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Queries
{
    public class GetUserByEmailQuery : IRequest<GetUserByEmailResult>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailResult
    {
        public User User { get; set; }
    }
    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResult>
    {
        private readonly IRepository _repository;

        public GetUserByEmailQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUserByEmailResult> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            DatabaseUser dbUser = await _repository.GetUserByEmail(request.Email, cancellationToken);
            return dbUser == null
                ? new GetUserByEmailResult()
                : new GetUserByEmailResult
                {
                    User = new User
                    {
                        Id = dbUser.Id.ToString(),
                        FirstName = dbUser.FirstName,
                        SecondName = dbUser.SecondName,
                        Role = dbUser.Role,
                        Email = dbUser.Email
                    }
                };
        }
    }
}