using System;
using Domain.Abstractions;
using Domain.Errors;

namespace Domain.Shared
{
	public sealed class EmptyResult : IResult
	{
		public EmptyResult()
		{
		}

		public EmptyResult(IError error) : base(error) {

		}

		public static implicit operator EmptyResult(IError error) => new EmptyResult(error);
		//public static implicit operator EmptyResult => new EmptyResult();
	}
}

