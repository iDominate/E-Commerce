using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Queries.GetPaginatedProduct;

public sealed record GetPaginatedProductQuery(int Page,
int PageSize,
Guid? BrandId,
Guid? CategoryId) :
IRequest<IApplicationResult>,
ICacheable
{
    public string CacheKey => $"products-{CategoryId}-{BrandId}-{Page}-{PageSize}";

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}