using Ecommerce.Domain.Models.BrandAggregate;

namespace Ecommerce.Application.BrandCQRS.Dtos;

public sealed record BrandDto(
    Guid Id,
    string Name
)
{
    public static BrandDto ToDto(Brand brand)
        => new BrandDto(brand.Id, brand.Name.Value);

    public static IEnumerable<BrandDto> ToDtoList(IEnumerable<Brand> brands)
        => brands.Select(ToDto);
};