using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.DeactivateProduct;

public sealed class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand,
IApplicationResult>
{
    private IRepository<Product> _repo;

    public DeactivateProductCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == request.ProductId);
        var products = await _repo.GetEntitiesAsync(spec);
        products.First().Deactivate();
        await _repo.SaveChangesAsync();
        return ApplicationResult<bool>.Success(true);        
    }
}