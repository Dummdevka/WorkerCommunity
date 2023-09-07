using System;
using Domain.Abstractions;

namespace Domain.Errors
{
	public class InternalError : IError
	{
		public InternalError(string error) : base(error)
		{
		}
	}
}

