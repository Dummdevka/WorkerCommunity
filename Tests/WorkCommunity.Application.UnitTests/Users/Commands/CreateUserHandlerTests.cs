using System;
using Application.Abstractions;
using Application.Abstrations;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using Domain.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Users.Commands
{
	public class CreateUserHandlerTests
	{
		private readonly UserManagerMock _userManager;
		private readonly Mock<ICachingService> _cachingService;
		private readonly Mock<IDbContext> _dbContext;
		public CreateUserHandlerTests()
		{
			_userManager = new UserManagerMock();
			_cachingService = new();
			_dbContext = new();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			var command = new CreateUserCommand("Lara", "Johnson", 21, "test@admin.com", "Junior web developer", "Laratest");
			var handler = new CreateUserHandler(_userManager.Mock.Object, _cachingService.Object, _dbContext.Object);
			_userManager.MockCreateAsync();

			//Execute
			Result<int> result = await handler.Handle(command, default);

			//Compare
			Assert.False(result.IsError);
		}

		[Fact]
		public async Task Handle_Should_ReturnFailureResult_WhenPasswordIsInvalid() {
			//Setup
			var command = new CreateUserCommand("Lara", "Johnson", 21, "admin@admin.com", "Junior web developer", "Laratest");
			var handler = new CreateUserHandler(_userManager.Mock.Object, _cachingService.Object, _dbContext.Object);
			string errorMessage = "Email already in use.";
			_userManager.MockCreateAsync(errorMessage);

			//Execute
			Result<int> result = await handler.Handle(command, default);

			//Compare
			Assert.True(result.IsError);
			Assert.Equal(result.Error.Errors.First(), errorMessage);
		}
	}
}

