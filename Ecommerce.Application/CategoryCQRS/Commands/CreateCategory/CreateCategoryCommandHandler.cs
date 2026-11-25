using Ecommerce.Application.CategoryCQRS.Commands.CreateCategory;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.CategoryAggregate;
using MediatR;


public sealed class CreateCategoryCommandHandler(IRepository<Category>
 repository) 
: IRequestHandler<CreateCategoryCommand, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var nameResult = CategoryName.Create(request.Name);
        var slugResult = CategorySlug.Create(request.Slug);
        var newCategory = Category.Create(nameResult.Value!, slugResult.Value!);
        return ApplicationResult<CategoryDto>.Success(CategoryDto.ToDto(await repository.CreateAsync(newCategory)));       
    }
}