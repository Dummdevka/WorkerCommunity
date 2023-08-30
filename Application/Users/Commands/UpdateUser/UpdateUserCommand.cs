using System;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.UpdateUser
{
	public record UpdateUserCommand(User user) : IRequest<User>;
}

