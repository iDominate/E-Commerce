using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.DecreaseProductStock;

public sealed record DecreaseProductStockCommand(Guid ProductId, int DecreaseBy)
: IRequest<IApplicationResult>;