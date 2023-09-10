using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Queries.GetFilteredRequests
{
	public record GetFilteredRequestsQuery : IRequest<Result<List<Request>>>
	{
		public RequestType? Type;

		public int? UserId;
		
		public string? Title;
		
		public string? Description;
		
		public bool? Completed;
	}
}

