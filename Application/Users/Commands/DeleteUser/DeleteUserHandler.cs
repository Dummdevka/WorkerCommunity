using System;
using Application.Abstractions;
using Application.Abstrations;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.DeleteUser
{
	public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		public DeleteUserHandler(IDbContext db, ICachingService cache) {
			_db = db;
			_cache = cache;
		}

		public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
			User? user = await _db.Users.FindAsync(request.id);
			if (user is not null) {
				_db.Users.Remove(user);
				await _db.SaveChangesAsync(cancellationToken);
				_cache.RemoveRecordsByKeyPattern(User.cacheKey);
			}
		}

	}
	
}

