using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, IApplicationResult>
{
    private IRepository<Product> _repo;

    public GetProductByIdQueryHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productSpec = new Specification<Product>();
        productSpec.AddCriteria(p => p.Id == request.ProductId);
        var result = await this._repo.GetEntitiesAsync(productSpec);
        return ApplicationResult<ProductDto>.Success(ProductDto.ToDto(result.First()));
       
    }
}