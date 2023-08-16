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

		public DbSet<User> Users {
			get; set;
		}

		public DbSet<Message> Messages {
			get; set;
		}

		public DbSet<Request> Requests {
			get; set;
		}

		public DbSet<Kitchen> Kitchen {
			get; set;
		}

		public DbSet<ParkingSlot> ParkingSlots {
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

