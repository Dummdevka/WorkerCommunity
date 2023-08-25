using System;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUsers
{
	public record GetUsersQuery : IRequest<List<User>>, ICacheableQuery
	{
		public bool skipCaching => false;

		public string cacheKey => User.cacheKey;

		public TimeSpan? absoluteExpiration => TimeSpan.FromHours(1);

		public TimeSpan? unusedExpiration => null;
	}
}

