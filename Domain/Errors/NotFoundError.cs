using System;
using Domain.Abstractions;

namespace Domain.Errors
{
	public class NotFoundError : IError
	{	
		public NotFoundError(string error) : base(error)
		{
		}
	}
}

