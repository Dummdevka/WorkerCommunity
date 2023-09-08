using System;
using Application.Requests.Queries;
using Domain.Entities;
using Domain.Shared;
using Infrastructure.Persistance;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Requests.Queries
{
	public class GetRequestsHandlerTests
	{
		public DbContextMock _mock {
			get; set;
		}

		public GetRequestsHandlerTests()
		{
			_mock = new("GetRequests");
			_mock.seedRequests();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var query = new GetRequestsQuery();
			var handler = new GetRequestsHandler(context);

			//Execute
			Result<List<Request>> result = await handler.Handle(query, default);

			//Validate
			Assert.False(result.IsError);
			Assert.True(result.Value.Count() == _mock.requests.Count());
		}
	}
}

