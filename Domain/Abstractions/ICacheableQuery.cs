using System;
namespace Domain.Abstractions
{
	public interface ICacheableQuery
	{
		bool skipCaching {
			get; 
		}

		string cacheKey {
			get;
		}

		//Type value

		TimeSpan? absoluteExpiration {
			get; 
		}

		TimeSpan? unusedExpiration {
			get;
		}
	}
}

