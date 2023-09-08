using System;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Commands.DeleteRequest
{
	public record DeleteRequestCommand(int id) : IRequest<EmptyResult>;
}

