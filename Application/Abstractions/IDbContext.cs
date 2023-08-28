using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions
{
	public interface IDbContext
	{
		DbSet<User> Users {
			get; set;
		}

		DbSet<ParkingSlot> ParkingSlots {
			get; set;
		}

		DbSet<Kitchen> Kitchen {
			get; set;
		}

		DbSet<Request> Requests {
			get; set;
		}

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

	}
}

