﻿using System;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Queries
{
	public record GetRequestsQuery() : IRequest<Result<List<Request>>>, ICacheableQuery
	{
		public bool skipCaching => false;

		public string cacheKey => Request.cacheKey;

		public TimeSpan? absoluteExpiration => TimeSpan.FromHours(1);

		public TimeSpan? unusedExpiration => null;
	}
}

