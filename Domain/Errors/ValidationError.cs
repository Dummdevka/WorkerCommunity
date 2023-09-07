using System;
using Domain.Abstractions;

namespace Domain.Errors
{
	public class ValidationError : IError
	{
		public ValidationError(string error) : base(error)
		{
		}

		public ValidationError(List<string> errors) : base(errors) {
		}
	}
}

