
using System.Text.Json;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheResponseAsync(string CacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(CacheKey);
            if(cachedResponse.IsNullOrEmpty) return null;
            return cachedResponse;
        }

        public async Task ResponseCacheAsync(string CacheKey, object Response, TimeSpan TimeToLive)
        {
            if(Response == null) return;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializedResponse = JsonSerializer.Serialize(Response,options);
            await _database.StringSetAsync(CacheKey,serializedResponse,TimeToLive);
        }
    }
}