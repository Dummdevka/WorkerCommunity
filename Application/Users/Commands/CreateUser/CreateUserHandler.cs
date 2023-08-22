using System;
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
	public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
	{
		public IDbContext _db;

		public CreateUserHandler(IDbContext db) {
			_db = db;
		}

		public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			User newUser = new User() {
				FirstName = request.firstname,
				LastName = request.lastname,
				Age = request.age,
				Email = request.email,
				Position = request.position
			};

			await _db.Users.AddAsync(newUser);

			return newUser.Id;
		}
	}
}

