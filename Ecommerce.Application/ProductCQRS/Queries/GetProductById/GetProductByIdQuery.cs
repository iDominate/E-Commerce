using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid ProductId) :
IRequest<IApplicationResult>, ICacheable
{
    public string CacheKey => $"product-{ProductId}";

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}