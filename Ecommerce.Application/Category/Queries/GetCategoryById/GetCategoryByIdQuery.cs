using MediatR;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<Category>> { }