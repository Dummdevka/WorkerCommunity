using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Tests.WorkCommunity.Application.UnitTests.Mocks
{
	public class DbContextMock
	{
		public List<User> users {
			get; set;
		}

		public List<Request> requests {
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

		public void seedRequests() {
			using var context = new CommunityDbContext(contextOptions);
			seedUsers();
			requests = new() {
				new Request() {
					UserId = users.First().Id,
					Title = "Need a day off",
					Description = "Tommorrow pls",
					RequestType = RequestType.DayOff
				},
				new Request() {
					UserId = users.First().Id,
					Title = "Need an iphone",
					Description = "Tommorrow pls",
					RequestType = RequestType.ItemNeeded
				},
				new Request() {
					UserId = users.Last().Id,
					Title = "Need a course on C#",
					Description = "Tommorrow pls",
					RequestType = RequestType.Learning
				},
				new Request() {
					UserId = users.Last().Id,
					Title = "Need a day off",
					Description = "Tommorrow pls",
					RequestType = RequestType.DayOff
				}
			};

			context.AddRange(requests);
			context.SaveChanges();
		}
	}
}

