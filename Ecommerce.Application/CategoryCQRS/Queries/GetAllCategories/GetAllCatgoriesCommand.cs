using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.CategoryCQRS.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery :
 IRequest<IApplicationResult>, ICacheable
{
    public string CacheKey => $"categories";

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}