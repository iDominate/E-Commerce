using Ecommerce.Application.Abstractions;
using MediatR;

public sealed class CachingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheable
{
    private readonly ICacheService _cacheService;

    public CachingBehavior(
        ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var cachedResponse =
            await _cacheService.GetAsync<TResponse>(
                request.CacheKey);

        if (cachedResponse is not null)
        {
            return cachedResponse;
        }

        var response = await next();

        await _cacheService.SetAsync(
            request.CacheKey,
            response,
            request.Expiration
            );

        return response;
    }
}