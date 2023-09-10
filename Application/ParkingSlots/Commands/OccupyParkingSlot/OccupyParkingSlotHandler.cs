using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ParkingSlots.Commands.OccupyParkingSlot
{
	public class OccupyParkingSlotHandler : IRequestHandler<OccupyParkingSlotCommand, Result<ParkingSlot>>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		public OccupyParkingSlotHandler(IDbContext db, ICachingService cache)
		{
			_db = db;
			_cache = cache;
		}

		public async Task<Result<ParkingSlot>> Handle(OccupyParkingSlotCommand request, CancellationToken cancellationToken)
		{
			ParkingSlot? slot = await _db.ParkingSlots.FindAsync(request.slotId, cancellationToken);
			User? user = _db.Users.Include("ParkingSlot").FirstOrDefault(u => u.Id == request.userId);
			if (slot is null)
				return new NotFoundError("Parking slot not found.");
			if (user is null)
				return new NotFoundError("User not found.");

			if(user.ParkingSlot is not null)
				user.ParkingSlot.UserId = null;

			slot.UserId = request.userId;
			await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(ParkingSlot.cacheKey);
			return slot;
		}
	}
}

