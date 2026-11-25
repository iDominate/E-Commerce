namespace Ecommerce.Application.ProductCQRS.Dtos;

using Ecommerce.Domain.Models.ProductAggregate;

public sealed class ProductDto(
    Guid Id,
    string Name,
    string Slug,
    string Description,
    decimal Price,
    int Stock,
    bool IsActive,
    IEnumerable<string> ImagesUrls
)
{
    public static ProductDto ToDto(Product product) =>
        new ProductDto(product.Id, product.Name.Value, product.Slug.Value,
         product.Description,
        product.Price!.Amount, 
        product.Stock, 
        product.IsActive, 
        product.Images.Select(i => i.Url));

    public static IEnumerable<ProductDto> ToDto(IEnumerable<Product> products) =>
        products.Select(ToDto);
}