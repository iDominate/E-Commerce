using Ecommerce.Application.Category.Errors;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed class UpdateCategoryCommandHandler(IRepository<Category> repository) : IRequestHandler<UpdateCategoryCommand, Result<Category?>>
{
    public async Task<Result<Category?>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        ISpecification<Category> specification = new Specification<Category>();
        specification.AddCriteria(c => c.Id == request.Id);
        var result = await repository.GetEntitiesAsync(specification);
        if(result.Count() == 0)
        {
            return Result<Category?>.Failure([CategoryErrors.NotFound]);
        }
        
        result.First().UpdateValues(request.Name, request.Slug);
        await repository.SaveChangesAsync();
        return Result<Category?>.Success(result.First());
    }
}