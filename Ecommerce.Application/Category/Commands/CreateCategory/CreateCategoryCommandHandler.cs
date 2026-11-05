using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed class CreateCategoryCommandHandler(IRepository<Category>
 repository) 
: IRequestHandler<CreateCategoryCommand, ApplicationResult<Category>>
{
    public async Task<ApplicationResult<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(request.Name);
        var slugResult = Slug.Create(request.Slug);
        List<Error> errors = new List<Error>(); 
        if(!nameResult.IsSuccess)
        {
            var nameError = new Error(nameResult.Code, nameResult.Message!, ErrorType.Validation);
            errors.Add(nameError);      
        }

        if(!slugResult.IsSuccess)
        {
            var slugError = new Error(slugResult.Code, slugResult.Message!, ErrorType.Validation);
            errors.Add(slugError);
        }
        if(errors.Count != 0)
        {
            return ApplicationResult<Category>.Failure(errors.ToArray());
        }
        var newCategory = Category.Create(nameResult.Object, slugResult.Object);
        return ApplicationResult<Category>.Success(await repository.CreateAsync(newCategory));       
    }
}