using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.UpdateUser
{
	public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
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

		public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			User? user = _db.Users.FirstOrDefault(u => u.Id == request.user.Id);
			if (user is null) {
				throw new KeyNotFoundException("User not found");
			}

			if (request.user.Email != user.Email) {
				await ValidateEmail(user, request.user.Email);
			}
			_db.Users.Entry(user).CurrentValues.SetValues(request.user);
			int result = await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(User.cacheKey);
			user.ToString();
			return user;	
		}

		protected async Task ValidateEmail(User user, string email) {
			User? emailSearch = _db.Users.FirstOrDefault(u => u.Email == email);
			if (emailSearch is not null)
				throw new FluentValidation.ValidationException("Email is already used.");	
		}
	}
}

