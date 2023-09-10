using System;
using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Entities;
using Domain.Extensions;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ParkingSlots.Queries.GetFilteredParkingSlots
{
	public class GetFilteredParkingSlotsHandler : IRequestHandler<GetFilteredParkingSlotsQuery, Result<List<ParkingSlot>>>
	{
		private readonly IDbContext _db;

		public GetFilteredParkingSlotsHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<Result<List<ParkingSlot>>> Handle(GetFilteredParkingSlotsQuery request, CancellationToken cancellationToken) {
			List<ParkingSlot> slots = new();
			Expression<Func<ParkingSlot, bool>> filter = default;
			if (request.userId is not null) {
				Expression<Func<ParkingSlot, bool>> userFilter = s => s.UserId == request.userId;
				filter = filter is null ? userFilter : filter.ConcatAdd(userFilter);
			}

			if (request.Occupied is not null) {
				Expression<Func<ParkingSlot, bool>> occupiedFilter = s => s.UserId == null;
				if (request.Occupied == true)
					occupiedFilter = s => s.UserId != null;

				filter = filter is null ? occupiedFilter : filter.ConcatAdd(occupiedFilter);
			}
			slots = await _db.ParkingSlots.Where(filter).AsQueryable().Include("OccupiedBy").ToListAsync();
			return slots;
		}
	}
}

