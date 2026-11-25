using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.ChangeProductPrice;


public sealed record ChangeProductPriceCommand(Guid ProductId,
decimal Price) : IRequest<IApplicationResult>;