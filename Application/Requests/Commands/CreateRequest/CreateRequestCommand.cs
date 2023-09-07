using System;
using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Commands.CreateRequest
{
	public record CreateRequestCommand(RequestType type, string title, string description, int createdBy) : IRequest<Result<int>>;
}

