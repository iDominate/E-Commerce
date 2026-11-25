using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.IncreaseProductStock;

public sealed record IncreaseProductStockCommand(Guid ProductId,
int IncreaseBy) : IRequest<IApplicationResult>;