using System;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.UpdateUserPassword
{
	public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand>
	{
		private readonly UserManager<User> _userManager;

		public UpdateUserPasswordHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
		{
			User? user = await _userManager.FindByIdAsync(request.id);
			if (user is null)
				throw new KeyNotFoundException("User not found!");
			var result = await _userManager.ChangePasswordAsync(user, request.oldPassword, request.newPassword);
	
			if (!result.Succeeded)
				throw new InvalidUserCredentialsException(result.Errors);
		}
	}
}

