using System;
using Application.Abstractions;
using Application.Abstrations;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.CreateUser
{
	public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
	{
		private readonly UserManager<User> _userManager;
		private readonly ICachingService _cache;
		private readonly IDbContext _db;

		public CreateUserHandler(UserManager<User> userManager, ICachingService cache, IDbContext db) {
			_userManager = userManager;
			_cache = cache;
			_db = db;
		}

		public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			User newUser = new User() {
				FirstName = request.firstname,
				LastName = request.lastname,
				Age = request.age,
				Email = request.email,
				Position = request.position,
				UserName = request.email,
				SecurityStamp = Guid.NewGuid().ToString("D")
			};

			var result = await _userManager.CreateAsync(newUser, request.password);
			if (!result.Succeeded)
				throw new InvalidUserCredentialsException(result.Errors);
			await _userManager.AddToRoleAsync(newUser, "Worker");
			_cache.RemoveRecordsByKeyPattern(User.cacheKey);

			return newUser.Id;
		}
	}
}

