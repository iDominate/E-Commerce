using Ecommerce.Domain.Models.BrandAggregate;

namespace Ecommerce.Application.BrandCQRS.Dtos;

public sealed record BrandDto(
    Guid Id,
    string Name,
    string Slug
)
{
    public static BrandDto ToDto(Brand brand)
        => new BrandDto(brand.Id, brand.Name.Value, brand.Slug.Value);

    public static IEnumerable<BrandDto> ToDtoList(IEnumerable<Brand> brands)
        => brands.Select(ToDto);
};