using System;
using Domain.Errors;

namespace Domain.Abstractions
{
	public abstract class IResult
	{
		public bool IsError {
			get; private set;
		}

		public IError Error {
			get; private set;
		}

		public IResult() {}

		public IResult(IError error)
		{
			IsError = true;
			Error = error;
		}

		public void SetError(IError error) {
			IsError = true;
			Error = error;
		}
	}
}

