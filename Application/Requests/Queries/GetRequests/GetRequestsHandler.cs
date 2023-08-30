using System;
using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries
{
	public class GetRequestsHandler : IRequestHandler<GetRequestsQuery, List<Request>>
	{
		private readonly IDbContext _db;

		public GetRequestsHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<List<Request>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
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

			if (!_db.Requests.Any()) {
				return new List<Request>();
			}
			List<Request> requests = filter is null ? await _db.Requests.AsQueryable().ToListAsync() : await _db.Requests.Where(filter).AsQueryable().ToListAsync();

			return requests;
		}
	}
}

