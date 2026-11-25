using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.UpdateProduct;


public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public UpdateProductCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productSpec = new Specification<Product>();
        productSpec.AddCriteria(p => p.Id == request.ProductId);
        var result = await _repo.GetEntitiesAsync(productSpec);
        var existingProduct = result.First();
        await _repo.SaveChangesAsync();
        return ApplicationResult<ProductDto>.Success(ProductDto.ToDto(existingProduct));
    }
}