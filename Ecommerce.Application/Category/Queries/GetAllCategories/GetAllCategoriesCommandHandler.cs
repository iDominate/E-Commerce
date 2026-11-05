using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllCategoriesCommandHandler(IRepository<Category> repository) : IRequestHandler<GetAllCategoriesCommand, ApplicationResult<IEnumerable<Category>>>
{
        public async Task<ApplicationResult<IEnumerable<Category>>> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
    {
        return ApplicationResult<IEnumerable<Category>>.
        Success(await repository.GetEntitiesAsync(new Specification<Category>()));
    
    }
}