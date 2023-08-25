using System;
using System.Reflection.Emit;
using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
	public class CommunityDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IDbContext
	{
		//public CommunityDbContext() {

		//}
		//private UserManager<User> _userManager;
		public CommunityDbContext(DbContextOptions<CommunityDbContext> options) : base(options)
		{
			//_userManager = userManager;
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

		protected override async void OnModelCreating(ModelBuilder model) {
			base.OnModelCreating(model);
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

			model.Entity<ParkingSlot>()
				.HasOne(s => s.OccupiedBy)
				.WithOne(u => u.ParkingSlot)
				.HasForeignKey<ParkingSlot>(s => s.UserId)
				.HasPrincipalKey<User>(u => u.Id);

			//User user = 
			//var hasher = new PasswordHasher<IdentityUser>();
			//model.Entity<User>()
			//	.HasData(new User() {
			//		Id = 1,
			//		FirstName = "Test",
			//		LastName = "Test",
			//		Email = "test@test.com",
			//		Position = "Tester",
			//		Age = 23,
			//		PasswordHash = hasher.HashPassword(null, "secret"),
			//		UserName = "admin",
			//		NormalizedUserName = "admin",
			//		NormalizedEmail = "test@test.com",
			//		SecurityStamp = Guid.NewGuid().ToString("D")
			//	});
		}

	}
}

