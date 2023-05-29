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
        ///         "firstName": "First Name",
        ///         "secondName": "Second Name",
        ///         "role": "Customer",
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
        /// Get user by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>User information</returns>
        /// <response code="200">Returns user information</response>
        /// <response code="404">User not found</response>
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(GetUserByEmailResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<IActionResult> GetUserByEmail(string email,
            CancellationToken cancellationToken = default)
        {
            GetUserByEmailQuery query = new()
            {
                Email = email
            };

            GetUserByEmailResult result = await _mediator.Send(query, cancellationToken);

            return result.User == null
                ? NotFound(new ErrorModel
                {
                    Message = $"User with email {email} not found"
                })
                : Ok(new GetUserByEmailResponse()
                {
                    User = result.User
                });
        }

        /// <summary>
        /// Update user by id
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="request">Update information</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Nothing</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /users/{id}
        ///     {
        ///         "firstName": "Updated First Name",
        ///         "secondName": "Updated Second Name",
        ///         "email": "updatedExample@gmail.com"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If user updated successfully</response>
        /// <response code="404">User not found</response>
        [HttpPatch("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId,
            [FromBody] UpdateUserByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            UpdateUserByIdCommand command = new()
            {
                Id = userId,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                Email = request.Email
            };

            try
            {
                _ = await _mediator.Send(command, cancellationToken);
            }
            catch (System.Exception)
            {
                return NotFound(new ErrorModel
                {
                    Message = $"User with {userId} not found"
                });
                throw;
            }
            return NoContent();
        }
    }
}