using Ecommerce.Application.Category.Commands.DeleteCategory;
using Ecommerce.Application.Category.Errors;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed class DeleteCategoryCommandHandler(IRepository<Category> repository) : IRequestHandler<DeleteCategoryCommand, Result<Guid?>>
{
    public async Task<Result<Guid?>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var specification = new Specification<Category>();
        specification.AddCriteria(c => c.Id == request.Id);
        var guidResult  = await repository.RemoveEntityAsync(specification);
        return guidResult is true ? Result<Guid?>.Success(request.Id) : Result<Guid?>.
        Failure([CategoryErrors.NotFound]);
    }
}