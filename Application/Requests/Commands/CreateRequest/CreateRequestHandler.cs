using System;
using Application.Abstractions;
using Domain.Enums;
using MediatR;

namespace Application.Requests.Commands.CreateRequest
{
	public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, int>
	{
		private readonly IDbContext _db;

		public CreateRequestHandler(IDbContext db)
		{
			_db = db;
		}

		public Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

