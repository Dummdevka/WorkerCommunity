using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Commands.DeleteRequest
{
	public class DeleteRequestHandler : IRequestHandler<DeleteRequestCommand, EmptyResult>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		public DeleteRequestHandler(IDbContext db, ICachingService cache)
		{
			_db = db;
			_cache = cache;
		}

		public async Task<EmptyResult> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
		{
			Request? result = await _db.Requests.FindAsync(request.id);
			if (result is null)
				return new NotFoundError("Request not found.");
			_db.Requests.Remove(result);
			await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(Request.cacheKey);
			return new();
			 
		}
	}
}

