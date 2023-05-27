using System;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

namespace FoodFetch.Domain.Commands
{
    public class UpdateUserByIdCommand : IRequest<UpdateUserByIdResult>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserByIdResult
    {
    }

    internal class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, UpdateUserByIdResult>
    {
        private readonly IRepository _repository;

        public UpdateUserByIdCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateUserByIdResult> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            DatabaseUser user = await _repository.GetUserById(request.Id, cancellationToken) ??
                throw new ArgumentException($"User with id {request.Id} not found");
            DatabaseUser userToUpdate = new()
            {
                Id = user.Id,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                Email = request.Email
            };

            await _repository.UpdateUserById(userToUpdate, cancellationToken);

            return new();
        }
    }
}