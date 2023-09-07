using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Users.Commands.DeleteUser
{
	public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, EmptyResult>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		public DeleteUserHandler(IDbContext db, ICachingService cache) {
			_db = db;
			_cache = cache;
		}

		public async Task<EmptyResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
			User? user = await _db.Users.FindAsync(request.id);
			if (user is null)
				return new NotFoundError("User not found.");
			else {
				_db.Users.Remove(user);
				await _db.SaveChangesAsync(cancellationToken);
				_cache.RemoveRecordsByKeyPattern(User.cacheKey);
				return new EmptyResult();
			}
		}

	}
	
}

