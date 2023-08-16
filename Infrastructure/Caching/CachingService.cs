using System;
using System.Text.Json;
using Application.Abstrations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Caching
{
	public class CachingService : ICachingService
	{
		IDistributedCache _cache;
		ConnectionMultiplexer _mutex;
		public CachingService(IDistributedCache cache, IConfiguration config)
		{
			_cache = cache;
			_mutex = ConnectionMultiplexer.Connect(config.GetConnectionString("Redis"));
		}

		public async Task<T?> GetDataAsync<T>(string key, CancellationToken cancellationToken = default)
		where T : class
		{
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
		CancellationToken cancellationToken = default)
			where T : class
		{
			var options = new DistributedCacheEntryOptions();
			options.AbsoluteExpirationRelativeToNow = absoluteExpTime ?? TimeSpan.FromHours(1);
			options.SlidingExpiration = unusedExpTime;

			string json = JsonSerializer.Serialize<T>(data);
			await _cache.SetStringAsync(key, json, cancellationToken);

		}

		public void RemoveRecordsByKeyPattern(string pattern)
		{
			var server = _mutex.GetServer("localhost", 5002);
			foreach (var key in server.Keys(pattern: pattern + '*'))
			{
				_cache.Remove(key);
			}
		}
	}
}

