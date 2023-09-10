using System;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Commands.DeleteParkingSlot
{
	public record DeleteParkingSlotCommand(int id) : IRequest<EmptyResult>;
}

