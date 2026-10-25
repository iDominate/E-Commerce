using MediatR;

namespace Ecommerce.Application.Category.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Result<Guid?>>;