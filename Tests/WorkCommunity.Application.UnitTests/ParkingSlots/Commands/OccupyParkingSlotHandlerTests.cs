using System;
using Application.Abstrations;
using Application.ParkingSlots.Commands.OccupyParkingSlot;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.ParkingSlots.Commands
{
	public class OccupyParkingSlotHandlerTests
	{
		public Mock<ICachingService> _cache;
		public DbContextMock _mock;

		public OccupyParkingSlotHandlerTests()
		{
			_cache = new();
			_mock = new("OccupyParkingSlot");
			_mock.seedParkingSlots();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup 
			using var context = new CommunityDbContext(_mock.contextOptions);
			int slotId = _mock.slots.First().Id;
			int userId = _mock.users.Last().Id;
			var command = new OccupyParkingSlotCommand(slotId, userId);
			var handler = new OccupyParkingSlotHandler(context, _cache.Object);

			//Execute
			Result<ParkingSlot> result = await handler.Handle(command, default);

			//Assert
			Assert.False(result.IsError);
			Assert.True(result.Value.UserId == userId);
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_WhenSlotNotFound() {
			//Setup 
			using var context = new CommunityDbContext(_mock.contextOptions);
			int slotId = _mock.slots.Last().Id + 1;
			int userId = _mock.users.Last().Id;
			var command = new OccupyParkingSlotCommand(slotId, userId);
			var handler = new OccupyParkingSlotHandler(context, _cache.Object);

			//Execute
			Result<ParkingSlot> result = await handler.Handle(command, default);

			//Assert
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
			Assert.True(result.Error.Errors.First() == "Parking slot not found.");
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_WhenUserNotFound() {
			//Setup 
			using var context = new CommunityDbContext(_mock.contextOptions);
			int slotId = _mock.slots.Last().Id;
			int userId = _mock.users.Last().Id + 1;
			var command = new OccupyParkingSlotCommand(slotId, userId);
			var handler = new OccupyParkingSlotHandler(context, _cache.Object);

			//Execute
			Result<ParkingSlot> result = await handler.Handle(command, default);

			//Assert
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
			Assert.True(result.Error.Errors.First() == "User not found.");
		}
	}
}

