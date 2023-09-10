using System;
using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Domain.Shared;

namespace Application.Requests.Queries
{
	public class GetRequestsHandler : IRequestHandler<GetRequestsQuery, Result<List<Request>>>
	{
		private readonly IDbContext _db;

		public GetRequestsHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<Result<List<Request>>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
		{
			List<Request> requests = await _db.Requests.AsQueryable().ToListAsync();

			return requests;
		}
	}
}

