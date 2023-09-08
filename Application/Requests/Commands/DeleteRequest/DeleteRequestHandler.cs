using System;
using Application.Abstractions;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Commands.DeleteRequest
{
	public class DeleteRequestHandler : IRequestHandler<DeleteRequestCommand, EmptyResult>
	{
		private readonly IDbContext _db;

		public DeleteRequestHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<EmptyResult> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
		{
			Request? result = await _db.Requests.FindAsync(request.id);
			if (result is null)
				return new NotFoundError("Request not found.");
			_db.Requests.Remove(result);
			await _db.SaveChangesAsync(cancellationToken);
			return new();
			 
		}
	}
}

