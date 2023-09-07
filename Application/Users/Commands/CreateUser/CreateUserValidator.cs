using System;
using FluentValidation;
namespace Application.Users.Commands.CreateUser
{
	public class CreateUserValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserValidator()
		{
			RuleFor(u => u.firstname)
				.MaximumLength(50)
				.NotEmpty();

			RuleFor(u => u.lastname)
				.MaximumLength(50)
				.NotEmpty();

			RuleFor(u => u.age)
				.InclusiveBetween(14, 59)
				.NotEmpty();

			RuleFor(u => u.position)
				.MaximumLength(300)
				.NotEmpty();

		}
	}
}

