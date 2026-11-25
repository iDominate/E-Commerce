using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.IncreaseProductStock;

public sealed class IncreaseProductStockCommandHandler :
IRequestHandler<IncreaseProductStockCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public IncreaseProductStockCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(IncreaseProductStockCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == request.ProductId);
        var result = await _repo.GetEntitiesAsync(spec);
        var existingProduct = result.First();
        existingProduct.IncreaseStock(request.IncreaseBy);
        await _repo.SaveChangesAsync();
        return ApplicationResult<ProductDto>.Success(ProductDto.ToDto(existingProduct));
    }
}