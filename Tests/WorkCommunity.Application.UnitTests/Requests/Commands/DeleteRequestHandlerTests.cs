using System;
using Application.Abstrations;
using Application.Requests.Commands.DeleteRequest;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Requests.Commands
{
	public class DeleteRequestHandlerTests
	{
		public DbContextMock _mock {
			get; set;
		}

		public Mock<ICachingService> _cache;

		public DeleteRequestHandlerTests()
		{
			_mock = new("DeleteRequest");
			_mock.seedRequests();
			_cache = new();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var command = new DeleteRequestCommand(_mock.requests.First().Id);
			var handler = new DeleteRequestHandler(context, _cache.Object);

			//Execution
			EmptyResult result = await handler.Handle(command, default);

			//Validation
			Assert.False(result.IsError);
			Assert.True(context.Requests.Count() == _mock.requests.Count() - 1);
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_WhenRequestNotFound() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var command = new DeleteRequestCommand(_mock.requests.Last().Id+1);
			var handler = new DeleteRequestHandler(context, _cache.Object);

			//Execution
			EmptyResult result = await handler.Handle(command, default);

			//Validation
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
		}
	}
}

