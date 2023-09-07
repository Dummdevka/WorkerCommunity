using System;
using Domain.Shared;
using MediatR;

namespace Application.Users.Commands.DeleteUser
{
	public record DeleteUserCommand(int id) : IRequest<EmptyResult>;
}

