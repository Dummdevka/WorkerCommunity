using System;
using Application.Abstrations;
using Application.Users.Commands.DeleteUser;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Users.Commands
{
	public class DeleteUserHandlerTests
	{
		public Mock<ICachingService> _cachingService {
			get; set;
		}

		private readonly DbContextMock _mock;

		public DeleteUserHandlerTests()
		{
			_cachingService = new();
			_mock = new("DeleteUser");
			//if
			_mock.seedUsers();
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_OnUserNotFound() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			User user = context.Users.Last();
			var command = new DeleteUserCommand(user.Id + 1);
			var handler = new DeleteUserHandler(context, _cachingService.Object);

			//Execute
			EmptyResult result = await handler.Handle(command, default);

			//Validate
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
			context.Database.EnsureDeleted();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			User user = context.Users.First();
			var command = new DeleteUserCommand(user.Id);
			var handler = new DeleteUserHandler(context, _cachingService.Object);

			//Execute
			EmptyResult result = await handler.Handle(command, default);

			//Validate
			Assert.False(result.IsError);
			Assert.True(context.Users.Count() == (_mock.users.Count - 1));
		}
		
	}
}

