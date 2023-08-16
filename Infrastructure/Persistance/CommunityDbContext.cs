using System;
using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
	public class CommunityDbContext : DbContext, IDbContext
	{
		//public CommunityDbContext() {

		//}
		public CommunityDbContext(DbContextOptions options) : base(options)
		{
		}

		DbSet<User> Users {
			get; set;
		}

		DbSet<Message> Messages {
			get; set;
		}

		DbSet<Request> Requests {
			get; set;
		}

		DbSet<Kitchen> Kitchen {
			get; set;
		}

		DbSet<ParkingSlot> ParkingSlots {
			get; set;
		}

		protected override void OnModelCreating(ModelBuilder model) {
			model.Entity<Request>(entity =>
			{
				entity.Property(e => e.RequestType)
					.HasConversion(x => (int)x, x => (RequestType)x);
			});

			model.Entity<Kitchen>(entity =>
			{
				entity.HasNoKey();
			});

			model.Entity<Message>()
				.HasOne(e => e.From)
				.WithMany(e => e.MessagesSent)
				.HasForeignKey(e => e.FromUserId)
				.OnDelete(DeleteBehavior.ClientCascade);

			model.Entity<Message>()
				.HasOne(e => e.To)
				.WithMany(e => e.MessagesReceived)
				.HasForeignKey(e => e.ToUserId)
				.OnDelete(DeleteBehavior.ClientCascade);	
		}
	}
}

