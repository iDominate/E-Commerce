namespace Ecommerce.Application.BrandCQRS.Commands.DeleteBrandByIdCommand;

using Ecommerce.Application.Shared;
using MediatR;

public sealed record DeleteBrandByIdCommand(Guid Id) : IRequest<IApplicationResult>;