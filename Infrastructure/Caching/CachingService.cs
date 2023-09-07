using System;
using System.Text.Json;
using Application.Abstrations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Infrastructure.Caching
{
	public class CachingService : ICachingService
		//where T : class
	{
		private readonly IDistributedCache _cache;
		ConnectionMultiplexer _mutex;
		private readonly IConfiguration _config;
		private readonly ILogger<CachingService> _logger;

		public CachingService(IDistributedCache cache, IConfiguration config, ILogger<CachingService> logger)
		{
			_cache = cache;
			_mutex = ConnectionMultiplexer.Connect(config.GetConnectionString("Redis"));
			_config = config;
			_logger = logger;
		}

		public async Task<T?> GetDataAsync<T>(string key, CancellationToken cancellationToken = default)
		where T : class {
			var json = await _cache.GetStringAsync(key, cancellationToken);
			if (json is null)
				return null;

			T? data = JsonSerializer.Deserialize<T>(json);
			
			return data;
		}

		public async Task SetDataAsync<T>(
		string key,
		T data,
		TimeSpan? absoluteExpTime = null,
		TimeSpan? unusedExpTime = null,
		CancellationToken cancellationToken = default) where T : class
		{
			var options = new DistributedCacheEntryOptions();
			options.AbsoluteExpirationRelativeToNow = absoluteExpTime ?? TimeSpan.FromHours(1);
			options.SlidingExpiration = unusedExpTime;

			string json = JsonSerializer.Serialize(data);
			await _cache.SetStringAsync(key, json, cancellationToken);

		}

		public void RemoveRecordsByKeyPattern(string entity, CancellationToken cancellationToken = default)
		{
			string pattern = _config.GetSection("RedisPrefix").Value + entity;
			var server = _mutex.GetServer("localhost", 5002);
			var keys = server.Keys(pattern: pattern + '*');
			foreach (var key in keys) {
				_logger.LogInformation("CACHE KEY: " + key);
				_mutex.GetDatabase().KeyDelete(key);	
			}
		}

		public async Task RemoveRecord(string key) {
			await _cache.RemoveAsync(key);
		}
	}
}

