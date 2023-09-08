﻿using Domain.Abstractions;
using Domain.Errors;
using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<IResult>
		where TResponse : IResult, new()
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) 
		{
			_validators = validators;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	    {
			if (_validators.Any()) {
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

				var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
				if (failures.Count() != 0) {
					TResponse response = new();
					response.SetError(new ValidationError(failures.Select(f => f.ErrorMessage).ToList()));
					return response;

				}
			}
			return await next();
		}
	}
}

