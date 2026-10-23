using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;

public class GetCategoryByIdQueryHandler(IRepository<Category> repository) : IRequestHandler<GetCategoryByIdQuery, Result<Category>>
{
    public async Task<Result<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Specification<Category> specification = new Specification<Category>();
        specification.AddCriteria(x => x.Id == request.Id);

        var category = await repository.GetEntitiesAsync(specification);
        if(category == null || !category.Any())
        {
            var error = new Error("CategoryNotFound", "Category not found", ErrorType.NotFound);
            return Result<Category>.Failure([error]);
            
        }

        return Result<Category>.Success(category.First());
    }
}
