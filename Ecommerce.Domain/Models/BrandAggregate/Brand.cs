using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using Ecommerce.Domain.Models.ProductAggregate;
namespace Ecommerce.Domain.Models.BrandAggregate;

public sealed class Brand : AggregateRoot
{
    public BrandName Name { get; private set; }
    public BrandSlug Slug { get; private set; }
    
    public ICollection<ProductAggregate.Product> Products { get; private set; } = [];
    private Brand(BrandName name)
    {
        Name = name;
        Slug = BrandSlug.Create(name.Value).Value!;
    }
    private Brand(BrandName name, IEnumerable<ProductAggregate.Product>? products = default)
    {
        Name = name;
        Slug = BrandSlug.Create(name.Value).Value!;
        Products = new List<ProductAggregate.Product>(products!);
    }
    public void UpdateName(BrandName name)
    {
        Name = name;
        Slug = BrandSlug.Create(name.Value).Value!;
    }

    public static Brand Create(BrandName name, IEnumerable<ProductAggregate.Product>? products = null)
    {
        products ??= [];
        return new Brand(name, products);
    }  

};