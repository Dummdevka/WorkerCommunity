using System;
using Application.Users.Commands.UpdateUser;
using FluentValidation;

namespace Application.Users.Commands
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserValidator()
		{
			RuleFor(u => u.user.FirstName)
				.NotEmpty()
				.MaximumLength(60);

			RuleFor(u => u.user.LastName)
			.NotEmpty()
			.MaximumLength(60);

			RuleFor(u => u.user.Age)
				.ExclusiveBetween(14, 69);

			RuleFor(u => u.user.Email)
				.EmailAddress()
				.NotEmpty();
		}
	}
}

