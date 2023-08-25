using System;
using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace Domain.Exceptions
{
	public class InvalidUserCredentialsException : Exception
	{
		public IEnumerable<IdentityError> errors;
		public InvalidUserCredentialsException(IEnumerable<IdentityError> errors)
		{
			this.errors = errors;
		}
	}
}

