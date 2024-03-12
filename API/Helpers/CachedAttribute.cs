using System.Text;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CachedAttribute(int TimeToLiveSeconds)
        {
            _timeToLiveSeconds = TimeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var Cachekey = GeenrateCachekeyFromrequest(context.HttpContext.Request);
            var cachedResponse = await CacheService.GetCacheResponseAsync(Cachekey);
            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentresult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentresult;
                return;
            }
            var executedContext = await next();
            if(executedContext.Result is OkObjectResult okObjectResult)
            {
                await CacheService.ResponseCacheAsync(Cachekey,okObjectResult.Value,TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private string GeenrateCachekeyFromrequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");

            }
            return keyBuilder.ToString();
        }
    }
}