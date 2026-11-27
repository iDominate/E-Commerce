using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.BrandCQRS.Commands.CreateBrandCommand;

public sealed record CreateBrandCommand(string Name) : IRequest<IApplicationResult>;