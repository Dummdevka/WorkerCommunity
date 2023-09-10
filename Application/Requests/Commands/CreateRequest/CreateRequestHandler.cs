using System;
using Application.Abstractions;
using Application.Abstrations;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Shared;
using MediatR;
//using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Commands.CreateRequest
{
	public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, Result<int>>
	{
		private readonly IDbContext _db;
		private readonly ICachingService _cache;

		//private readonly UserManager<User> _userManager;

		public CreateRequestHandler(IDbContext db, ICachingService cache)
		{
			_db = db;
			_cache = cache;
			//_userManager = userManager;
		}

		public async Task<Result<int>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
		{
			User? user = await _db.Users.FindAsync(request.createdBy);
			if (user is null)
				return new NotFoundError("User not found.");

			Request newRequest = new() {
				UserId = request.createdBy,
				Title = request.title,
				Description = request.description,
				RequestType = request.type
			};

			var result = await _db.Requests.AddAsync(newRequest);
			await _db.SaveChangesAsync(cancellationToken);
			_cache.RemoveRecordsByKeyPattern(Request.cacheKey);
			return newRequest.Id;
		}
	}
}

