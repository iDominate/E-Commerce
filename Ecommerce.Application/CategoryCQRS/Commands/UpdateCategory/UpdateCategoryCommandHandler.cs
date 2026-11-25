using Ecommerce.Application.CategoryCQRS.Errors;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.CategoryAggregate;
using MediatR;


namespace Ecommerce.Application.CategoryCQRS.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(IRepository<Category> repository) :
 IRequestHandler<UpdateCategoryCommand, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        ISpecification<Category> specification = new Specification<Category>();
        specification.AddCriteria(c => c.Id == request.Id);
        var result = await repository.GetEntitiesAsync(specification);
        if(result.Count() == 0)
        {
            return ApplicationResult<Category?>.Failure([CategoryErrors.NotFound]);
        }
        var newName = CategoryName.Create(request.Name);
        var newSlug = CategorySlug.Create(request.Slug);
        result.First().UpdateValues(newName.Value!, newSlug.Value!);
        await repository.SaveChangesAsync();
        return ApplicationResult<CategoryDto>.Success(CategoryDto.ToDto(result.First()));
    }
}