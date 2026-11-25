using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid ProductId,
    string? Name,
    string? Slug,
    string? Description,
    decimal? Price,
    Guid? CategoryId,
    Guid? BrandId
) : IRequest<IApplicationResult>;