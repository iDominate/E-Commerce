using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.AtivateProduct;

public sealed record ActivateProductCommand(Guid ProductId) :
IRequest<IApplicationResult>;