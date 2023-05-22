using System;
using System.Threading;
using System.Threading.Tasks;

using FoodFetch.Contracts.Enums;
using FoodFetch.Domain.Commands;
using FoodFetch.Domain.Database.Models;
using FoodFetch.Domain.Repositories;

using MediatR;

using Moq;

namespace FoodFetch.UnitTests.Commands
{
    public class CreateUserCommandTests
    {
        private readonly Mock<IRepository> _repositoryMock = new();
        private readonly IRequestHandler<CreateUserCommand, CreateUserResult> _handler;

        public CreateUserCommandTests()
        {
            _handler = new CreateUserCommandHandler(_repositoryMock.Object);
        }

        [Theory]
        [InlineData(Role.Customer)]
        [InlineData(Role.Deliveryman)]
        public async Task HandleShouldAddUserToRepository(Role role)
        {
            // Arrange
            string firstName = Guid.NewGuid().ToString();
            string secondName = Guid.NewGuid().ToString();
            string email = $"{Guid.NewGuid():N}@gmail.com";
            CreateUserCommand command = new()
            {
                FirstName = firstName,
                SecondName = secondName,
                Role = role,
                Email = email
            };

            // Act
            _ = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(x => x.AddUser(
                It.Is<DatabaseUser>(p => p.FirstName == command.FirstName
                && p.SecondName == secondName
                && p.Role == role
                && p.Email == email),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}