using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.CategoryAggregate;
using MediatR;

namespace Ecommerce.Application.CategoryCQRS.Queries.GetAllCategories;
public class GetAllCategoriesQueryHandler(IRepository<Category> repository) : 
IRequestHandler<GetAllCategoriesQuery, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetEntitiesAsync(new Specification<Category>());
        return ApplicationResult<IEnumerable<CategoryDto>>.
        Success(CategoryDto.ToDtoList(categories));
    
    }
}