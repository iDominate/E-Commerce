using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryCommand));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR((c) => c.RegisterServicesFromAssembly(typeof(DI).Assembly));
       return services; 
    }
}