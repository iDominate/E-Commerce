using Ecommerce.Application.BrandCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using MediatR;

namespace Ecommerce.Application.BrandCQRS.Commands.CreateBrandCommand;

public sealed class CreateBrandCommandHandler(IRepository<Brand> repository) : IRequestHandler<CreateBrandCommand, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandName = BrandName.Create(request.Name);
        var brand = Brand.Create(brandName.Value!);
        await repository.CreateAsync(brand);
        return ApplicationResult<BrandDto>.Success(BrandDto.ToDto(brand));
    }
}