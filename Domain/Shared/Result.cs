using System;
using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Errors;

namespace Domain.Shared
{
	public sealed class Result <T> : IResult
	{
		public T Value {
			get;
		}	

		[JsonConstructor]
		public Result(T value)
		{
			Value = value;
		}

		public Result(IError error) : base(error) {}

		public static implicit operator Result<T>(T value) => new Result<T>(value); 

		public static implicit operator Result<T>(IError error) => new Result<T>(error); 
	}
}

