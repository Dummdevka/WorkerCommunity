using System;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
	public class CommunityDbContext : DbContext, IDbContext
	{
		public CommunityDbContext(DbContextOptions options) : base(options)
		{
		}

		DbSet<User> Users {
			get; set;
		}

		DbSet<Message> Messages {
			get; set;
		}
	}
}

