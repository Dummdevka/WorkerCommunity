using System;
using MediatR;

namespace Application.Users.Commands.DeleteUser
{
	public record DeleteUserCommand(int id) : IRequest;
}

