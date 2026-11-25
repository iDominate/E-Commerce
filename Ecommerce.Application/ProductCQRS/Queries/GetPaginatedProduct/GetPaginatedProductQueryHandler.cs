using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Queries.GetPaginatedProduct;

public sealed class GetPaginatedProductQueryHandler : IRequestHandler<GetPaginatedProductQuery,
 IApplicationResult>
{
    private IRepository<Product> _repo;

    public GetPaginatedProductQueryHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> 
    Handle(GetPaginatedProductQuery request,
    CancellationToken cancellationToken)
    {
        var productSpec = new Specification<Product>();
        if(request.BrandId is not null)
        {
            productSpec.AddCriteria((p) => request.BrandId == p.BrandId);
        } else if(request.CategoryId is not null)
        {
            productSpec.AddCriteria((p) => request.CategoryId == p.CategoryId);
        } else if(request.BrandId is not null && request.BrandId is not null)
        {
            productSpec.AddCriteria((p) => request.BrandId == p.BrandId &&
            request.CategoryId == p.CategoryId);
        }
        productSpec.ApplyPagination(request.Page * (request.PageSize -1), request.PageSize);
        var result = await this._repo.GetEntitiesAsync(productSpec);
        return ApplicationResult<IEnumerable<ProductDto>>.Success(result.Select(ProductDto.ToDto));
        
    }
}