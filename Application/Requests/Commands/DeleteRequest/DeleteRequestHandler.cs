using System;
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.DeleteRequest
{
	public class DeleteRequestHandler : IRequestHandler<DeleteRequestCommand>
	{
		private readonly IDbContext _db;

		public DeleteRequestHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
		{
			Request? result = await _db.Requests.FindAsync(request.id);
			if (result is null)
				throw new KeyNotFoundException("Request could not be found.");
			_db.Requests.Remove(result);
			await _db.SaveChangesAsync(cancellationToken);
			 
		}
	}
}

