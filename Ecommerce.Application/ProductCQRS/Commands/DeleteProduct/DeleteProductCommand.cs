using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : IRequest<IApplicationResult>;