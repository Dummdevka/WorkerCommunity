using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Commands.DeleteParkingSlot
{
	public class DeleteParkingSlotHandler : IRequestHandler<DeleteParkingSlotCommand, EmptyResult>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		public DeleteParkingSlotHandler(IDbContext db, ICachingService cache)
		{
			_db = db;
			_cache = cache;
		}

		public async Task<EmptyResult> Handle(DeleteParkingSlotCommand request, CancellationToken cancellationToken)
		{
			ParkingSlot? slot = await _db.ParkingSlots.FindAsync(request.id);
			if (slot is null)
				return new NotFoundError("Parking slot not found.");
			_db.ParkingSlots.Remove(slot);
			await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(ParkingSlot.cacheKey);
			return new EmptyResult();
		}
	}
}

