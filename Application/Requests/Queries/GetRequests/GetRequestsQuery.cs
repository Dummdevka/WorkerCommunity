using System;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Requests.Queries
{
	public record GetRequestsQuery(
		RequestType? Type = null,
		int? UserId = null,
		string? Title = null, 
		string? Description = null, 
		bool? Completed = null) : IRequest<List<Request>>;
}

