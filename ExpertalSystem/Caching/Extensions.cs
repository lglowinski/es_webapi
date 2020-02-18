using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ExpertalSystem.Caching
{
    public static class Extensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = new RedisSettings();
            configuration.GetSection(nameof(redisSettings)).Bind(redisSettings);
            services.AddSingleton(redisSettings);

            var options = ConfigurationOptions.Parse(redisSettings.ConnectionString);

            services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(redisSettings.ConnectionString));

            services.AddStackExchangeRedisCache(o =>
            {
                o.ConfigurationOptions = options;

            });
            services.AddSingleton<ICacheProvider, CacheProvider>();
            return services;
        }
    }
}
