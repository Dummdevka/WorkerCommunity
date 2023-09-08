using System;
using Application.Abstractions;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Commands.UpdateRequest
{
	public class UpdateRequestHandler : IRequestHandler<UpdateRequestCommand, Result<Request>>
	{
		private readonly IDbContext _db;

		public UpdateRequestHandler(IDbContext db)
		{
			_db = db;
		}

		public async Task<Result<Request>> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
		{
			int requestId = request.Id ?? request.Request.Id;
			Request? target = _db.Requests.Find(requestId);
			if (target is null)
				return new NotFoundError("Request not found.");
		
			if (request.Data is not null) {
				var properties = typeof(Request).GetProperties().ToList();
				foreach (var prop in properties) {
					if (request.Data.ContainsKey(prop.Name)) {
						target.GetType().GetProperty(prop.Name).SetValue(target, request.Data[prop.Name]);
					}
				}
			} else {
				_db.Requests.Entry(target).CurrentValues.SetValues(request.Request);
			}	
			await _db.SaveChangesAsync(cancellationToken);
			return target;
		}
	}
}

