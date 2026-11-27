using MediatR;
using Ecommerce.Application.Shared;

namespace Ecommerce.Application.BrandCQRS.Commands.UpdateBrandByIdCommand;

#pragma warning disable CS9113 // Parameter is unread.
public sealed record UpdateBrandByIdCommand(Guid Id, string Name) : IRequest<IApplicationResult>;
#pragma warning restore CS9113 // Parameter is unread.