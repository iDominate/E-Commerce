using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Application.CategoryCQRS.Errors;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.CategoryAggregate;
using MediatR;

public class GetCategoryByIdQueryHandler(IRepository<Category> repository) : 
IRequestHandler<GetCategoryByIdQuery, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Specification<Category> specification = new Specification<Category>();
        specification.AddCriteria(x => x.Id == request.Id);

        var category = await repository.GetEntitiesAsync(specification);
        if(category == null || !category.Any())
        {
            return ApplicationResult<Category>.Failure([CategoryErrors.NotFound]);
            
        }

        return ApplicationResult<CategoryDto>.Success(CategoryDto.ToDto(category.First()));
    }
}
