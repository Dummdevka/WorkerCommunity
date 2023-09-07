using System;
using Application.Abstractions;
using Application.Abstrations;
using Application.Users.Commands.UpdateUser;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Users.Commands
{
	public class UpdateUserHandlerTests
	{
		private readonly UserManagerMock _userManager;
		private readonly Mock<ICachingService> _cachingService;
		//private readonly Mock<IDbContext> _dbContext;
		private readonly DbContextMock _mock;

		public UpdateUserHandlerTests() {
			_userManager = new UserManagerMock();
			_cachingService = new();
			_mock = new("UpdateUsers");
			_mock.seedUsers();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			User user = _mock.users.First();
			string firstname = "John";
			string lastname = "Pepper";
			user.FirstName = firstname;
			user.LastName = lastname;  
			var command = new UpdateUserCommand(user);
			var handler = new UpdateUserHandler(context, _userManager.Mock.Object, _cachingService.Object);

			//Execute
			Result<User> result = await handler.Handle(command, default);

			//Validate
			Assert.False(result.IsError);
			Assert.True(result.Value.FirstName == firstname);
			Assert.True(result.Value.LastName == lastname);
		}

		[Fact]
		public async Task Handle_Should_ReturnFaulire_OnUserNotFound() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var user = _mock.users.First();
			int userId = _mock.users.Count + 1;
			user.Id = userId;

			var command = new UpdateUserCommand(user);
			var handler = new UpdateUserHandler(context, _userManager.Mock.Object, _cachingService.Object);

			//Execute
			Result<User> result = await handler.Handle(command, default);

			//Validate
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
			Assert.True(result.Error.Errors.First() == "User not found.");
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess_OnChangeEmail() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var user = _mock.users.First();
			string email = "favorite1@gmail.com";
			user.Email = email;
			var command = new UpdateUserCommand(user);
			var handler = new UpdateUserHandler(context, _userManager.Mock.Object, _cachingService.Object);

			//Execute
			Result<User> result = await handler.Handle(command, default);

			//Validate
			Assert.False(result.IsError);
			Assert.True(result.Value.Email == email);
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_OnChangeEmail() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var user = _mock.users.First();
			string email = _mock.users.Last().Email;

			user.Email = email;
			var command = new UpdateUserCommand(user);
			var handler = new UpdateUserHandler(context, _userManager.Mock.Object, _cachingService.Object);

			//Execute
			Result<User> result = await handler.Handle(command, default);

			//Validate
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(ValidationError));
			Assert.True(result.Error.Errors.First() == "Email had already been taken.");
		}
	}
}

