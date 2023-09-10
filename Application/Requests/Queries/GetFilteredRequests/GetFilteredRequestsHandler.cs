using System;
using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Entities;
using Domain.Extensions;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.GetFilteredRequests
{
	public class GetFilteredRequestsHandler : IRequestHandler<GetFilteredRequestsQuery, Result<List<Request>>>
	{
		private readonly IDbContext _db;

		public GetFilteredRequestsHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<Result<List<Request>>> Handle(GetFilteredRequestsQuery request, CancellationToken cancellationToken)
		{
			Expression<Func<Request, bool>> filter = default;

			if (request.Title is not null) {
				Expression<Func<Request, bool>> titleFilter = r => r.Title == request.Title;
				filter = filter is not null ? filter.ConcatAdd(titleFilter) : titleFilter;
			}

			if (request.Description is not null) {
				Expression<Func<Request, bool>> descriptionFilter = r => r.Description == request.Description;
				filter = filter is not null ? filter.ConcatAdd(descriptionFilter) : descriptionFilter;
			}

			if (request.Type is not null) {
				Expression<Func<Request, bool>> typeFilter = r => r.RequestType == request.Type;
				filter = filter is not null ? filter.ConcatAdd(typeFilter) : typeFilter;
			}

			if (request.UserId is not null) {
				Expression<Func<Request, bool>> userFilter = r => r.UserId == request.UserId;
				filter = filter is not null ? filter.ConcatAdd(userFilter) : userFilter;
			}
			if (request.Completed is not null) {
				Expression<Func<Request, bool>> completedFilter = r => r.Completed == request.Completed;
				filter = filter is not null ? filter.ConcatAdd(completedFilter) : completedFilter;
			}

			List<Request> requests = await _db.Requests.Where(filter).AsQueryable().ToListAsync();
			return requests;
		}
	}
}

