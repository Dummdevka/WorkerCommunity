using System;
using Application.Requests.Commands.UpdateRequest;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Requests.Commands
{
	public class UpdateRequestsHandlerTests
	{
		public DbContextMock _mock {
			get; set;
		}

		public UpdateRequestsHandlerTests()
		{
			_mock = new("UpdateRequest");
			_mock.seedRequests();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			Request request = _mock.requests.First();
			request.RequestType = RequestType.Learning;
			string title = "Changed title";
			request.Title = title;
			string description = "Changed desc";
			request.Description = description;

			var command = new UpdateRequestCommand(request);
			var handler = new UpdateRequestHandler(context);

			//Execution
			Result<Request> result = await handler.Handle(command, default);

			//Validation
			Assert.False(result.IsError);
			Assert.True(result.Value.RequestType == RequestType.Learning);
			Assert.True(result.Value.Title == title);
			Assert.True(result.Value.Description == description);
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_OnRequestNotFound() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			Request request = _mock.requests.Last();
			request.Id = request.Id + 1;

			var command = new UpdateRequestCommand(request);
			var handler = new UpdateRequestHandler(context);

			//Execution
			Result<Request> result = await handler.Handle(command, default);

			//Validation
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
		}
	}
}

