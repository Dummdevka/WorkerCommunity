using System;
using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers
{
	public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<List<User>>>
 	{		
		public IDbContext _db;


		public GetUsersHandler(IDbContext db) {
			_db = db;
		}

		public async Task<Result<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var user = await _db.Users.AsQueryable().ToListAsync();
			List<User> users = _db.Users.Any() ? await _db.Users.AsQueryable().ToListAsync(cancellationToken) : new();

			return users;
		}
	}
}

