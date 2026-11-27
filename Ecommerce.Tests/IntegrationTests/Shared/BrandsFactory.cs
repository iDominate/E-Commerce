using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;

namespace Ecommerce.Tests.IntegrationTests.Shared;

public static class BrandsFactory
{
    public static IEnumerable<Brand> Generate(int number = 10)
    {
        var faker = new Bogus.Faker<Brand>()
            .CustomInstantiator(f =>
            {
                var brandName = BrandName.Create(f.Company.CompanyName());
                return Brand.Create(brandName.Value!);
            });
        return faker.Generate(number);
    }
}