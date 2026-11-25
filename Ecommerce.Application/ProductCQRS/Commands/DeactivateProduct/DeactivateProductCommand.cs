using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.DeactivateProduct;

public sealed record DeactivateProductCommand(Guid ProductId)
: IRequest<IApplicationResult>;