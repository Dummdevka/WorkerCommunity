using System;
using FluentValidation;

namespace Application.Requests.Commands.CreateRequest
{
	public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
	{
		public CreateRequestValidator()
		{
			RuleFor(r => r.title)
				.NotEmpty()
				.MaximumLength(200);

			RuleFor(r => r.description)
				.MaximumLength(500);

			RuleFor(r => r.type)
				.NotNull();
		}
	}
}

