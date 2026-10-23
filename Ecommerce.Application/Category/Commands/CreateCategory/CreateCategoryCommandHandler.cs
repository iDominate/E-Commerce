using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed class CreateCategoryCommandHandler(IRepository<Category> repository) 
: IRequestHandler<CreateCategoryCommand, Result<Category>>
{
    public async Task<Result<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return Result<Category>.Success(await repository.CreateAsync(request.Category));       
    }
}