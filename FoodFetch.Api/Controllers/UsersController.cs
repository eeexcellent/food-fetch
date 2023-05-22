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
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="request">Information about user</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Identifier of newly created user</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /users
        ///     {
        ///         "firstName": "First Name"
        ///         "secondName": "Second Name"
        ///         "role": 0
        ///         "email": "example@gmail.com"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">User created</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [ProducesResponseType(typeof(CreateUserResponse), 201)]
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

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>User information</returns>
        /// <response code="200">Returns user information</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserByIdResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<IActionResult> GetUserById(string id,
            CancellationToken cancellationToken = default)
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