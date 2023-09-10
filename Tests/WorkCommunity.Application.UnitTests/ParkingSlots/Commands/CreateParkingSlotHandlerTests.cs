using System;
using Tests.WorkCommunity.Application.UnitTests.Mocks;

namespace Tests.WorkCommunity.Application.UnitTests.ParkingSlots.Commands
{
	public class CreateParkingSlotHandlerTests
	{
		public DbContextMock _mock {
			get; set;
		}

		public CreateParkingSlotHandlerTests()
		{
			_mock = new("OccupyParkingSlot");
		}
	}
}

