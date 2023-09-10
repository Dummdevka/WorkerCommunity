using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.ParkingSlots.Commands.CreateParkingSlot
{
	public class CreateParkingSlotHandler : IRequestHandler<CreateParkingSlotCommand, Result<int>>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		public CreateParkingSlotHandler(IDbContext db, ICachingService cache)
		{
			_db = db;
			_cache = cache;
		}

		public async Task<Result<int>> Handle(CreateParkingSlotCommand request, CancellationToken cancellationToken)
		{
			ParkingSlot slot = new() {
				Name = request.name
			};

			await _db.ParkingSlots.AddAsync(slot);
			await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(ParkingSlot.cacheKey);
			return slot.Id;
		}
	}
}

