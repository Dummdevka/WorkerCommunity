using System;
using Application.Abstrations;
using Application.ParkingSlots.Commands.CreateParkingSlot;
using Domain.Shared;
using Infrastructure.Persistance;
using Moq;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.ParkingSlots.Commands
{
	public class CreateParkingSlotHandlerTests
	{
		public DbContextMock _mock {
			get; set;
		}

		private readonly Mock<ICachingService> _cachingService;

		public CreateParkingSlotHandlerTests()
		{
			_mock = new("CreateParkingSlot");
			_mock.seedParkingSlots();
			_cachingService = new();
		}

		[Fact]
		public async Task Handle_Should_ReturnSuccess() {
			//Setup
			using var context = new CommunityDbContext(_mock.contextOptions);
			string name = "A-5";
			var command = new CreateParkingSlotCommand(name);
			var handler = new CreateParkingSlotHandler(context, _cachingService.Object);

			//Execute
			Result<int> result = await handler.Handle(command, default);

			//Assert
			Assert.False(result.IsError);
			Assert.True(context.ParkingSlots.Count() == _mock.slots.Count()+1);
		}
	}
}

