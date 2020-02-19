using System;
using System.Threading.Tasks;

namespace ExpertalSystem.Caching
{
    public interface ICacheProvider
    {
        /// <summary>
        /// Cache response by giving key, value and time to live
        /// </summary>
        /// <param name="key">Key of cached response</param>
        /// <param name="value">Value of cached response</param>
        /// <param name="timeToLive">Time after cached response gets dumped</param>
        /// <returns></returns>
        Task CacheResponseAsync(string key, object value, TimeSpan timeToLive);

        /// <summary>
        /// Gets cached response by key
        /// </summary>
        /// <param name="cacheKey">Key of response</param>
        /// <returns></returns>
        Task<string> GetCachedResponseAsync(string cacheKey);

        /// <summary>
        /// Drops all cache existing in REDIS database
        /// </summary>
        /// <returns></returns>
        Task DumpCache();
    }
}
