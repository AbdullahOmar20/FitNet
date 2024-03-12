
namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task ResponseCacheAsync(string CacheKey, object Response, TimeSpan TimeToLive);
        Task<string> GetCacheResponseAsync(string CacheKey);

    }
}