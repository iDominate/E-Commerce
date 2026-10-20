using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllCategoriesCommandHandler(IRepository<Category> repository, DbSet<Category> categories) : IRequestHandler<GetAllCategoriesCommand, Result<IEnumerable<Category>>>
{
    public async Task<Result<IEnumerable<Category>>> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
    {
        return Result<IEnumerable<Category>>.
        Success(await repository.GetAllEntitiesAsync<Category>(categories));
    
    }
}