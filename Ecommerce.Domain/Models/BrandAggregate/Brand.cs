using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using Ecommerce.Domain.Models.ProductAggregate;
namespace Ecommerce.Domain.Models.BrandAggregate;

public sealed class Brand : AggregateRoot
{
    public BrandName Name { get; private set; }
    public BrandSlug Slug { get; private set; }
    
    public ICollection<ProductAggregate.Product> Products { get; private set; } = [];
    private Brand(BrandName name, BrandSlug slug)
    {
        Name = name;
        Slug = slug;
    }
    private Brand(BrandName name, BrandSlug slug, IEnumerable<ProductAggregate.Product>? products = default)
    {
        Name = name;
        Slug = slug;
        Products = new List<ProductAggregate.Product>(products!);
    }

    public static Brand Create(BrandName name, BrandSlug slug, IEnumerable<ProductAggregate.Product>? products = null)
    {
        products ??= [];
        return new Brand(name, slug, products);
    }  

};