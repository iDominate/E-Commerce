using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.AtivateProduct;

public sealed class ActivateProductCommandHandler :
IRequestHandler<ActivateProductCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public ActivateProductCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == request.ProductId);
        var products = await _repo.GetEntitiesAsync(spec);
        products.First().Activate();
        await _repo.SaveChangesAsync();
        return ApplicationResult<bool>.Success(true);
    }
}