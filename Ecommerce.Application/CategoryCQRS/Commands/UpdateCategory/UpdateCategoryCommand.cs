using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.CategoryCQRS.Commands.UpdateCategory;
public sealed record UpdateCategoryCommand(Guid Id, string Name, string Slug) : IRequest<IApplicationResult>;
