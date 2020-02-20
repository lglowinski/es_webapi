using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExpertalSystem.Caching
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLive;

        public CachedAttribute(int timeToLive = 600)
        {
            _timeToLive = timeToLive;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisSettings>();
            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheProvider>();

            var key = CreateCacheKey(context.HttpContext.Request);
            var cachedValue = await cacheService.GetCachedResponseAsync(key);
            if (!string.IsNullOrEmpty(cachedValue))
            {
                var contentResult = new ContentResult
                {
                    ContentType = "application/json",
                    Content = cachedValue,
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var controllerResult = await next();

            if (controllerResult.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLive));
            }
        }

        private string CreateCacheKey(HttpRequest httpContextRequest)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{httpContextRequest.Path}");

            foreach (var (key, value) in httpContextRequest.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}:{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
