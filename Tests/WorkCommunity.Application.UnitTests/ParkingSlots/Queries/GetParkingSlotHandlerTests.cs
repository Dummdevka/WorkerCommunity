using System;
using Application.ParkingSlots.Queries.GetParkingSlots;
using Domain.Entities;
using Domain.Shared;
using Infrastructure.Persistance;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.ParkingSlots.Queries
{
	public class GetParkingSlotHandlerTests
	{
		public DbContextMock _mock;

		public GetParkingSlotHandlerTests()
		{
			_mock = new("GetParkingSlots");
			_mock.seedParkingSlots();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var query = new GetParkingSlotsQuery();
			var handler = new GetFilteredParkingSlotsHandler(context);

			//Execute
			Result<List<ParkingSlot>> result = await handler.Handle(query, default);

			//Assert
			Assert.False(result.IsError);
			Assert.True(result.Value.Count() == _mock.slots.Count());
		}
	}
}

