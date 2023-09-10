using System;
using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Entities;
using Domain.Extensions;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ParkingSlots.Queries.GetParkingSlots
{
	public class GetFilteredParkingSlotsHandler : IRequestHandler<GetParkingSlotsQuery, Result<List<ParkingSlot>>>
	{
		private readonly IDbContext _db;

		public GetFilteredParkingSlotsHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<Result<List<ParkingSlot>>> Handle(GetParkingSlotsQuery request, CancellationToken cancellationToken) 
	    {
			List<ParkingSlot> slots = await _db.ParkingSlots.AsQueryable().Include("OccupiedBy").ToListAsync();
			
			return slots;
		}
	}
}

