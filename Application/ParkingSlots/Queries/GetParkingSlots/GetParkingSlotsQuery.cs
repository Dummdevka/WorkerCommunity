using System;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Queries.GetParkingSlots
{
	public record GetParkingSlotsQuery : IRequest<Result<List<ParkingSlot>>>, ICacheableQuery
	{
		public bool skipCaching => false;

		public string cacheKey => ParkingSlot.cacheKey;

		public TimeSpan? absoluteExpiration => TimeSpan.FromHours(1);

		public TimeSpan? unusedExpiration => null;
	}
}

