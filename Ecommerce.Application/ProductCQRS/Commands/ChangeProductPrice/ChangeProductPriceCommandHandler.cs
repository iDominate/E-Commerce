using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.ChangeProductPrice;

public sealed class ChangeProductPriceCommandHandler :
IRequestHandler<ChangeProductPriceCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public ChangeProductPriceCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(ChangeProductPriceCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == request.ProductId);
        var exisitngProduct = await this._repo.GetEntitiesAsync(spec);
        exisitngProduct.First().ChangePrice(ProductPrice.
        Create(request.Price, CurrenyEnum.USD).Value!);
        await this._repo.SaveChangesAsync();
        return ApplicationResult<ProductPriceDto>.Success(ProductPriceDto.ToDto(exisitngProduct.First().Price!));
    }
}