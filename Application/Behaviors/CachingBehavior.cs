using System;
using Application.Abstrations;
using Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
	public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery
	where TResponse : class
    {
		protected readonly ICachingService _cache;
		protected readonly ILogger _logger;
		protected readonly IConfiguration _config;


		public CachingBehavior(ICachingService cache, ILogger<CachingBehavior<TRequest,TResponse>> logger, IConfiguration config)
		{
			_cache = cache;
			_logger = logger;
			_config = config;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (request.skipCaching)
				return await next();
			string key = request.cacheKey + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

			var cachedData = await _cache.GetDataAsync<TResponse>(key);
			if (cachedData is null) {
				_logger.LogWarning("Added to cache with key: " + key);
				TResponse response = await next();
				await _cache.SetDataAsync(key, response, request.absoluteExpiration, request.unusedExpiration);
				return response;
			}

			_logger.LogWarning("Fetched from cache with key: " + key);
			return cachedData;
		}
	}
}

