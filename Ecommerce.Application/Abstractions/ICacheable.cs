namespace Ecommerce.Application.Abstractions;

public interface ICacheable
{
    public string CacheKey { get;}
    public TimeSpan Expiration { get; }
}