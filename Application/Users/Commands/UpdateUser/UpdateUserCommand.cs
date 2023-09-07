using System;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Users.Commands.UpdateUser
{
	public record UpdateUserCommand(User user) : IRequest<Result<User>>;
}

