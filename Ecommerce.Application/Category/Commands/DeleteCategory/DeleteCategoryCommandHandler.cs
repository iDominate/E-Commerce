using Ecommerce.Application.Category.Commands.DeleteCategory;
using Ecommerce.Application.Category.Errors;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed class DeleteCategoryCommandHandler(IRepository<Category> repository) : IRequestHandler<DeleteCategoryCommand, ApplicationResult<Guid?>>
{
    public async Task<ApplicationResult<Guid?>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var specification = new Specification<Category>();
        specification.AddCriteria(c => c.Id == request.Id);
        var guidResult  = await repository.RemoveEntityAsync(specification);
        await repository.SaveChangesAsync();
        return guidResult is true ? ApplicationResult<Guid?>.Success(request.Id) : ApplicationResult<Guid?>.
        Failure([CategoryErrors.NotFound]);
    }
}