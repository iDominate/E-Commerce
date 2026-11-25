using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;

namespace Ecommerce.Tests.IntegrationTests.Shared;

public static class BrandsFactory
{
    public static IEnumerable<Brand> Generate(int number)
    {
        var faker = new Bogus.Faker<Brand>()
            .CustomInstantiator(f =>
            {
                var brandName = BrandName.Create(f.Company.CompanyName());
                var brandSlug = BrandSlug.Create(BrandSlug.Normalize(brandName.Value!.Value));
                return Brand.Create(brandName.Value, brandSlug.Value!);
            });
        return faker.Generate(number);
    }
}