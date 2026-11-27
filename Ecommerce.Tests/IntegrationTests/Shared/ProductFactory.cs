using Ecommerce.Domain.Models.ProductAggregate;
using Bogus;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using Ecommerce.Domain.Models.ProductAggregate.Entities;

namespace Ecommerce.Tests.IntegrationTests.Shared;

public static class ProductFactory
{
    public static IEnumerable<Product> Generate(int count = 10)
    {
        var faker = new Faker();
        var factory = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(
                ProductName.Create(f.Commerce.ProductName()).Value!,
                ProductSlug.Create(f.Commerce.ProductName()).Value!,
                f.Commerce.ProductDescription(),
                ProductPrice.Create(f.Random.Decimal(1, 100), CurrenyEnum.USD).Value!,
                Guid.NewGuid(),
                Guid.NewGuid(),
                f.Random.Number(1, 1000),
                f.Random.Bool()
            ));
        var result = factory.Generate(count);
        result.ForEach(p => p.AddImage(GenerateImage(p, faker)));

        return result;
    }

    private static ProductImage GenerateImage(Product product, Faker faker)
    {
        return ProductImage.Create(product.Id,
            faker.Image.PicsumUrl(),
            [.. faker.Random.Bytes(1000)],
            faker.System.FileType(),
            faker.Random.Bool(0.1f)).Value!;
    }
}