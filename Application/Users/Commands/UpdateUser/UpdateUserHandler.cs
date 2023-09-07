using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Errors;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.UpdateUser
{
	public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<User>>
	{
		private readonly IDbContext _db;
		private readonly UserManager<User> _userManager;
		private readonly ICachingService _cache;

		public UpdateUserHandler(IDbContext db, UserManager<User> userManager, ICachingService cache)
		{
			_db = db;
			_userManager = userManager;
			_cache = cache;
		}

		public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			User? user = _db.Users.FirstOrDefault(u => u.Id == request.user.Id);
			if (user is null) {
				return new NotFoundError("User not found.");
			}

			if (request.user.Email != user.Email) {
				User? emailSearch = _db.Users.FirstOrDefault(u => u.Email == request.user.Email);
				if (emailSearch is not null)
					return new ValidationError("Email had already been taken.");
			}
			_db.Users.Entry(user).CurrentValues.SetValues(request.user);
			int result = await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(User.cacheKey);
			user.ToString();
			return user;	
		}
	}
}

