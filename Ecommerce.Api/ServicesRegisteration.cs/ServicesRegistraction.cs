using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.Api.ServicesRegisteration.cs;


public static class ServicesRegisteration
{
    /// <summary>
    /// Configures the rate limiting service
    /// </summary>
    public static IServiceCollection AddRateLimitingService(this IServiceCollection services)
    {
        services.AddRateLimiter(o =>
        {
            o.AddSlidingWindowLimiter("api", configureOptions =>
            {
                configureOptions.PermitLimit = 100;
                configureOptions.Window =TimeSpan.FromMinutes(1);
                configureOptions.SegmentsPerWindow = 6;
                configureOptions.QueueLimit = 0;
            });
            o.AddFixedWindowLimiter("auth", configureOptions =>
            {
                configureOptions.PermitLimit = 5;
                configureOptions.Window = TimeSpan.FromMinutes(1);
                configureOptions.QueueLimit = 0;
            });
        });
        return services;   
    }
}