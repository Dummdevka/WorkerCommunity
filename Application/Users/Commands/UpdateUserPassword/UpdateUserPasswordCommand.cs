using System;
using MediatR;

namespace Application.Users.Commands.UpdateUserPassword
{
	public record UpdateUserPasswordCommand(string id, string oldPassword, string newPassword) : IRequest;
}

