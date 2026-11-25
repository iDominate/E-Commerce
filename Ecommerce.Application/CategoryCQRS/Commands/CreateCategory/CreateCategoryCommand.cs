using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.CategoryCQRS.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string Slug) : IRequest<IApplicationResult>{}