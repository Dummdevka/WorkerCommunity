using System;
using Application.Abstrations;
using Application.ParkingSlots.Commands.DeleteParkingSlot;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.ParkingSlots.Commands
{
	public class DeleteParkingSlotHandlerTests
	{
		public DbContextMock _mock;
		public Mock<ICachingService> _cache;

		public DeleteParkingSlotHandlerTests()
		{
			_mock = new("DeleteParkingSlot");
			_mock.seedParkingSlots();
			_cache = new();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var command = new DeleteParkingSlotCommand(_mock.slots.First().Id);
			var handler = new DeleteParkingSlotHandler(context, _cache.Object);

			//Execute
			EmptyResult result = await handler.Handle(command, default);

			//Assert
			Assert.False(result.IsError);
			//var count = 
			//var count2 = ;
			//bool a = count == count2;
			Assert.True((bool)(context.ParkingSlots.Count() == (_mock.slots.Count() - 1)));
		}

		[Fact]
		public async Task Handle_Should_ReturnFailure_WhenSlotNotFound() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			var command = new DeleteParkingSlotCommand(_mock.slots.Last().Id + 1);
			var handler = new DeleteParkingSlotHandler(context, _cache.Object);

			//Execute
			EmptyResult result = await handler.Handle(command, default);

			//Assert
			Assert.True(result.IsError);
			Assert.True(result.Error.GetType() == typeof(NotFoundError));
			Assert.True(context.ParkingSlots.Count() == _mock.slots.Count());
			context.Database.EnsureDeleted();
		}
	}
}

