using System;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Commands.OccupyParkingSlot
{
	public record OccupyParkingSlotCommand(int slotId, int userId) : IRequest<Result<ParkingSlot>>;
}

