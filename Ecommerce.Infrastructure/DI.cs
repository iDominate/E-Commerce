using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Infrastructure.Services.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;

public static class DI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString = null)
    {
        services.AddDbContext<AppDbContext>(o => o.UseSqlServer(
            connectionString
        ));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();
        return services;
    }

    public static IServiceCollection AddInfrastructureTesting(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("tests"));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();
        return services;
    }
}