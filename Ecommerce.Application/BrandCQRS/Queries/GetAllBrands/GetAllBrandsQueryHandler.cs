namespace Ecommerce.Application.BrandCQRS.Queries.GetAllBrands;

using MediatR;
using Ecommerce.Application.BrandCQRS.Dtos;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Application.Repositories.Contracts;

public sealed class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandDto>>
{
    private readonly IRepository<Brand> _brandRepository;

    public GetAllBrandsQueryHandler(IRepository<Brand> brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task<IEnumerable<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.
        GetEntitiesAsync(new Specification<Brand>());
        return BrandDto.ToDtoList(brands);
    }
}