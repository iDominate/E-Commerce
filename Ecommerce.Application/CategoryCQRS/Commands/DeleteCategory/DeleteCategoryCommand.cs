using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.CategoryCQRS.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<IApplicationResult>;