using System;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Users.Queries.GetUsers
{
	public record GetUsersQuery : IRequest<Result<List<User>>>, ICacheableQuery
	{
		public bool skipCaching => false;

		public string cacheKey => User.cacheKey;

		public TimeSpan? absoluteExpiration => TimeSpan.FromHours(1);

		public TimeSpan? unusedExpiration => null;
	}
}

