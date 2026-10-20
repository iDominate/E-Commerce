using MediatR;

public sealed record GetAllCategoriesCommand : IRequest<Result<IEnumerable<Category>>>;