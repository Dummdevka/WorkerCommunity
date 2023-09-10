using System;
using Application.Abstrations;
using Application.Requests.Commands.CreateRequest;
using Domain.Enums;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Requests.Commands
{
	public class CreateRequestHandlerTest
	{
		public DbContextMock _mock {
			get; set;
		}

		public Mock<ICachingService> _cache;

		public CreateRequestHandlerTest()
		{
			_mock = new DbContextMock("CreateRequest");
			_mock.seedRequests();
			_cache = new();
			//_mock.seedUsers();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var command = new CreateRequestCommand(RequestType.ItemNeeded,  "A request", "Dummy description", _mock.users.First().Id);
			var handler = new CreateRequestHandler(context, _cache.Object);

			//Execute
			Result<int> result = await handler.Handle(command, default);

			//Assert
			Assert.False(result.IsError);
			Assert.True(result.Value == _mock.requests.Last().Id + 1);
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_IfUserNotFound() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var command = new CreateRequestCommand(RequestType.ItemNeeded, "A request", "Dummy description", _mock.users.Last().Id + 1);
			var handler = new CreateRequestHandler(context, _cache.Object);

			//Execute
			Result<int> result = await handler.Handle(command, default);

			//Assert
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
		}
	}
}

