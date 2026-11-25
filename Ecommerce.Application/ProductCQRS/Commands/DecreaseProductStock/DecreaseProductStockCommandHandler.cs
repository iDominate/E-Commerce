using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.DecreaseProductStock;

public sealed class DecreaseProductStockCommandHandler :
IRequestHandler<DecreaseProductStockCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public DecreaseProductStockCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(DecreaseProductStockCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == request.ProductId);
        var product = await _repo.GetEntitiesAsync(spec);
        var existingProduct = product.First();
        existingProduct.DecreaseStock(request.DecreaseBy);
        await _repo.SaveChangesAsync();        
        return ApplicationResult<ProductDto>.Success(ProductDto.ToDto(existingProduct));
    }
}