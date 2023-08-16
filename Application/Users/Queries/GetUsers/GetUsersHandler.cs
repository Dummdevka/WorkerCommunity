using System;
using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers
{
	public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<User>>
 	{		
		public IDbContext _db;

		public GetUsersHandler(IDbContext db) {
			_db = db;
		}

		public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			List<User> users = _db.Users.Any() ? await _db.Users.AsQueryable().ToListAsync(cancellationToken) : new();

			return users;
		}
	}
}

