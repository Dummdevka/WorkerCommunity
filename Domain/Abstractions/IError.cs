using System;
namespace Domain.Abstractions
{
	public abstract class IError
	{
		public List<string> Errors {
			get; set;
		}

		public IError(string error)
		{
			Errors = new();
			Errors.Add(error);
		}

		public IError(List<string> errors) {
			Errors = errors;
		}
	}
}

