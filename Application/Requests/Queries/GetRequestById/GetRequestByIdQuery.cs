using System;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Queries.GetRequestById
{
	public record GetRequestByIdQuery(int id) : IRequest<Result<Request>>;
}

