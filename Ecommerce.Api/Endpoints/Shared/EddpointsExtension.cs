

using Ecommerce.Api.Endpoints.Shared;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services)
    {
        var endpoints =
            typeof(IEndpoint).Assembly
            .DefinedTypes
            .Where(t =>
                t is { IsAbstract: false, IsInterface: false }
                && typeof(IEndpoint).IsAssignableFrom(t));

        foreach (var endpoint in endpoints)
        {
            services.AddSingleton(
                typeof(IEndpoint),
                endpoint);
        }

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app)
    {
        var endpoints =
            app.Services.GetRequiredService<
                IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}