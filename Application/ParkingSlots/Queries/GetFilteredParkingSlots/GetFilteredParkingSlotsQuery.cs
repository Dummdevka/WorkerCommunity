using System;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Queries.GetFilteredParkingSlots
{
	public record GetFilteredParkingSlotsQuery : IRequest<Result<List<ParkingSlot>>>
	{
		public int? userId;

		public bool? Occupied;
	}
}

