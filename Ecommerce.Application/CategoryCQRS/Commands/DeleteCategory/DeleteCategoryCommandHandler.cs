using Ecommerce.Application.CategoryCQRS.Errors;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.CategoryAggregate;
using MediatR;

namespace Ecommerce.Application.CategoryCQRS.Commands.DeleteCategory;


public sealed class DeleteCategoryCommandHandler(IRepository<Category> repository) : 
IRequestHandler<DeleteCategoryCommand, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var specification = new Specification<Category>();
        specification.AddCriteria(c => c.Id == request.Id);
        var guidResult  = await repository.RemoveEntityAsync(specification);
        await repository.SaveChangesAsync();
        return guidResult is true ? ApplicationResult<Guid?>.Success(request.Id) : ApplicationResult<Guid?>.
        Failure(CategoryErrors.NotFound);
    }
}