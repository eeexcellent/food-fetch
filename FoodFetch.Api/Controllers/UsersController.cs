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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id, CancellationToken cancellationToken = default)
        {
            GetUserByIdQuery query = new()
            {
                Id = id
            };

            GetUserByIdResult result = await _mediator.Send(query, cancellationToken);

            return result.User == null
                ? NotFound(new ErrorModel
                {
                    Message = $"User with {id} not found"
                })
                : Ok(new GetUserByIdResponse()
                {
                    User = result.User
                });
        }
    }
}