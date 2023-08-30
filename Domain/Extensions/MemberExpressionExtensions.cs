using System;
using System.Linq.Expressions;

namespace Domain.Extensions
{
	public static class MemberExpressionExtensions
	{
		public static Expression<Func<TEnt, TType>> ConcatAdd<TEnt, TType>(
			this Expression<Func<TEnt, TType>> first, Expression<Func<TEnt, TType>> second) {
			return Expression.Lambda<Func<TEnt, TType>>(
				Expression.And(first.Body, second.Body)
			);
		}
	}
}

