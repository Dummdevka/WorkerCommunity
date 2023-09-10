using System;
using Application.Abstractions;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.GetRequestById
{
	public class GetRequestByIdHandler : IRequestHandler<GetRequestByIdQuery, Result<Request>>
	{
		private readonly IDbContext _db;

		public GetRequestByIdHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<Result<Request>> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
		{
			Request? result = _db.Requests.Include("CreatedBy").FirstOrDefault(r => r.Id == request.id);
			if (result is null)
				//throw new KeyNotFoundException("Request has could been found.");
				return new NotFoundError("Request not found.");
			return result;
		}
	}
}

