using Ecommerce.Application.Category.Errors;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed class UpdateCategoryCommandHandler(IRepository<Category> repository) : IRequestHandler<UpdateCategoryCommand, ApplicationResult<Category?>>
{
    public async Task<ApplicationResult<Category?>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        ISpecification<Category> specification = new Specification<Category>();
        specification.AddCriteria(c => c.Id == request.Id);
        var result = await repository.GetEntitiesAsync(specification);
        if(result.Count() == 0)
        {
            return ApplicationResult<Category?>.Failure([CategoryErrors.NotFound]);
        }
        var newName = Name.Create(request.Name);
        var newSlug = Slug.Create(request.Slug);
        var errors = new List<Error>();
        if(!newName.IsSuccess)
        {
            errors.Add(
            new Error("Category.InvalidName",
            "The input name is invalid", ErrorType.Validation));                 
        }

        if(!newSlug.IsSuccess)
        {
            errors.Add(new Error("Category.InvalidSlug",
            "The input slug is not valid",
            ErrorType.Validation));  
        }
        if(errors.Count != 0)
        {
            return ApplicationResult<Category?>.Failure(errors.ToArray());
        }
        result.First().UpdateValues(newName.Object, newSlug.Object);
        await repository.SaveChangesAsync();
        return ApplicationResult<Category?>.Success(result.First());
    }
}