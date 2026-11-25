using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public DeleteProductCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productSpec = new Specification<Product>();
        productSpec.AddCriteria(p => p.Id == request.ProductId);
        var result = await this._repo.GetEntitiesAsync(productSpec);
        return ApplicationResult<Guid>.Success(result.First().Id);
    }
}