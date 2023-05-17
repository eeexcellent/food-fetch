using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Http;
using FoodFetch.Domain.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace FoodFetch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request,
            CancellationToken cancellationToken = default)
        {
            CreateUserCommand command = new()
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                Role = request.Role,
                Email = request.Email
            };

            CreateUserResult result = await _mediator.Send(command, cancellationToken);

            return Created($"users/{result.Id}", new CreateUserResponse
            {
                Id = result.Id
            });
        }
    }
}