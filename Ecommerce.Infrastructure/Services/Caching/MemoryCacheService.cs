using Ecommerce.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services.Caching;

public sealed class MemoryCacheService : ICacheService
{
    private ILogger<MemoryCacheService> _logger;
    private IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache, ILogger<MemoryCacheService> logger)
    {
        _logger = logger;
        _cache = cache;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        _cache.TryGetValue(key, out T? value);
        _logger.LogInformation("Fetch object {Key} from cache", key);
        return Task.FromResult(value);
    }

    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        _logger.LogInformation("Removed object {Key} from cache", key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        _cache.Set<T>(key, value, expiration);
        _logger.LogInformation("Set a new object {key} with exporation {expiration}",
        key, expiration);
        return Task.CompletedTask;
    }
}