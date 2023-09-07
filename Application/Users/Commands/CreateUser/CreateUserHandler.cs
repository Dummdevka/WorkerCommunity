using System;
using Application.Abstractions;
using Application.Abstrations;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using Domain.Errors;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.CreateUser
{
	public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<int>>
	{
		private readonly UserManager<User> _userManager;
		private readonly ICachingService _cache;
		private readonly IDbContext _db;

		public CreateUserHandler(UserManager<User> userManager, ICachingService cache, IDbContext db) {
			_userManager = userManager;
			_cache = cache;
			_db = db;
		}

		public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

			IdentityResult result = await _userManager.CreateAsync(newUser, request.password);
			
			if (!result.Succeeded)
				return new ValidationError(result.Errors.Select(e => e.Description).ToList());
			await _userManager.AddToRoleAsync(newUser, "Worker");
			_cache.RemoveRecordsByKeyPattern(User.cacheKey);

			return newUser.Id;
		}
	}
}

