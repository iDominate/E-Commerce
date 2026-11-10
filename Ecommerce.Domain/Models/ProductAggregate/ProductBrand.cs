using Ecommerce.Domain.Models.Product.ValueObjects.Product;

namespace Ecommerce.Domain.Models.ProductAggregate;

public sealed class ProductBrand : AggregateRoot
{
    public Guid BrandId { get; private set; }
    public BrandName Name { get; private set; }
    public ICollection<Product>? Products { get; set; }

    private ProductBrand(BrandName name, IEnumerable<Product> products)
    {
    }

    public static ProductBrand Create(BrandName name, IEnumerable<Product>? products = null)
    {

        return new ProductBrand(name, products!);
    }  

};