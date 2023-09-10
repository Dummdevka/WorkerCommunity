using System;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Commands.CreateParkingSlot
{
	public record CreateParkingSlotCommand(string name) : IRequest<Result<int>>;
}

