using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Shared;
using MediatR;

public record GetCategoryByIdQuery(Guid Id) :
IRequest<IApplicationResult>, ICacheable
{
    public string CacheKey => $"category-{Id}";

    public TimeSpan Expiration => throw new NotImplementedException();
}