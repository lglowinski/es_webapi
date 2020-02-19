using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ExpertalSystem.Caching
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<CacheProvider> _logger;
        private readonly RedisSettings _settings;
        private readonly EndPoint _endpoint;

        public CacheProvider(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer, ILogger<CacheProvider> logger, RedisSettings settings)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
            _logger = logger;
            _settings = settings;
            _endpoint = connectionMultiplexer.GetEndPoints().FirstOrDefault();
        }

        public async Task CacheResponseAsync(string key, object value, TimeSpan timeToLive)
        {
            if (value == null)
            {
                return;
            }

            var serializedValue = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
            _logger.LogInformation($"Element cached at: {key}");
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            _logger.LogInformation($"Requested cached data under: {cacheKey}");
            var response = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(response) ? null : response;
        }

        public async Task DumpCache()
        {
            _logger.LogInformation("Trying to flush database");
            await _connectionMultiplexer.GetServer(_endpoint).FlushDatabaseAsync(_settings.Database);
            _logger.LogInformation("Database flushed");
        }
    }
}
