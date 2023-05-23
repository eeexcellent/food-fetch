using System;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Enums;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Commands
{
    public class UpdateUserCommand : IRequest<UpdateUserResult>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserResult
    {
        public string Id { get; set; }
    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IRepository _repository;

        public UpdateUserCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            DatabaseUser user = new()
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                Role = request.Role,
                Email = request.Email,
                Id = Guid.NewGuid()
            };

            await _repository.AddUser(user, cancellationToken);

            return new UpdateUserResult
            {
                Id = user.Id.ToString()
            };
        }
    }
}