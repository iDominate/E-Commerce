using Ecommerce.Application.CategoryCQRS.Commands.CreateCategory;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryCommand));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        services.AddMediatR((c) => c.RegisterServicesFromAssembly(typeof(DI).Assembly));
       return services; 
    }
}