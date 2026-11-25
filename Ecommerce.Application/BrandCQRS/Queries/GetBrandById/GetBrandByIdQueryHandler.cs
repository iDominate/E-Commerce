using Ecommerce.Application.BrandCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.BrandAggregate;
using MediatR;

namespace Ecommerce.Application.BrandCQRS.Queries.GetBrandById;

public sealed class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, IApplicationResult>
{
    private readonly IRepository<Brand> _brandRepository;
    public GetBrandByIdQueryHandler(IRepository<Brand> brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IApplicationResult> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Brand>();
        spec.AddCriteria(b => b.Id == request.BrandId);
        var brands = await _brandRepository.GetEntitiesAsync(spec);
        var result = BrandDto.ToDtoList(brands);
        return ApplicationResult<IEnumerable<BrandDto>>.Success(result);
    }
}