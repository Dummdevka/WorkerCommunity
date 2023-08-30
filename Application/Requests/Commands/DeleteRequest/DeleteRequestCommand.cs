using System;
using MediatR;

namespace Application.Requests.Commands.DeleteRequest
{
	public record DeleteRequestCommand(int id) : IRequest;
}

