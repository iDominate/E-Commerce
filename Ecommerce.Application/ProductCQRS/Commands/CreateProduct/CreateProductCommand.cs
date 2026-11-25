using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Slug,
    string Description,
    decimal Price,
    Guid CategoryId,
    Guid BrandId,
    int Stock
) : IRequest<IApplicationResult>;