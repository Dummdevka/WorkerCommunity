using System;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using Domain.Shared;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.Users.Queries
{
	public class GetUsersHandlerTests
	{
		public DbContextMock _mock {
			get; set;
		}

		private DbContextOptions<CommunityDbContext> contextOptions =
			new DbContextOptionsBuilder<CommunityDbContext>()
			.UseInMemoryDatabase(databaseName: "WorkerCommunityDB")
			.Options;

		//private readonly 
		public List<User> users {
			get; set;
		}

		public GetUsersHandlerTests()
		{
			_mock = new DbContextMock("GetUsers");
			_mock.seedUsers();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var query = new GetUsersQuery();
			var handler = new GetUsersHandler(context);

			//Execute
			Result<List<User>> result = await handler.Handle(query, default);

			//Assert
			Assert.False(result.IsError);
			Assert.True(_mock.users.Count == result.Value.Count);
		}
	}
}

