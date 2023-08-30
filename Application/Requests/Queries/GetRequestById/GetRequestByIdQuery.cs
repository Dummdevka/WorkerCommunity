using System;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Queries.GetRequestById
{
	public record GetRequestByIdQuery(int id) : IRequest<Request>;
}

