using System;
using System.Security;
using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Tests.WorkCommunity.Application.UnitTests.Mocks
{
	public class DbContextMock
	{
		public List<User> users {
			get; set;
		}

		public DbContextOptions<CommunityDbContext> contextOptions;
			

		public DbContextMock(string dbName) {
			contextOptions = new DbContextOptionsBuilder<CommunityDbContext>()
			.UseInMemoryDatabase(databaseName: dbName)
			.Options;
		}

		public void seedUsers() {
			using var context = new CommunityDbContext(contextOptions);
			//var user = context.Users.ToList();
			////if (context.Users.ToList().Count > 0)
			////	return;
			users = new() {
				new User() {
					FirstName = "Hanna",
					LastName = "Brown",
					Age = 43,
					Position = "Designer",
					Email = "hanna@gmail.com",
					UserName = "Hanna123",
					SecurityStamp = Guid.NewGuid().ToString("D")
				},
				new User() {
					FirstName = "Michael",
					LastName = "Dorey",
					Age = 23,
					Position = "HR",
					Email = "michael@gmail.com",
					UserName = "Michael123",
					SecurityStamp = Guid.NewGuid().ToString("D")
				},
				new User() {
					FirstName = "Karol",
					LastName = "Lott",
					Age = 36,
					Position = "Developer",
					Email = "karol@gmail.com",
					UserName = "Karol123",
					SecurityStamp = Guid.NewGuid().ToString("D")
				},
				new User() {
					FirstName = "Moritz",
					LastName = "Weiss",
					Age = 24,
					Position = "Manager",
					Email = "moritz@gmail.com",
					UserName = "Moritz123",
					SecurityStamp = Guid.NewGuid().ToString("D")
				}
			};
			context.AddRange(users);
			context.SaveChanges();
		}
	}
}

