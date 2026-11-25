using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.RemoveProductImage;

public sealed record RemoveProductImageCommand(Guid ProductId,
string ImageName) : 
IRequest<IApplicationResult>;