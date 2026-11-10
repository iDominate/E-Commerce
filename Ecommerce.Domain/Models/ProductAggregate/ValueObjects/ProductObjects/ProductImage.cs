namespace Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;

public sealed class ProductImage
{
    public Guid Id { get; private set; }
    public string Url { get; private set; }
    public Product? Product { get; private set; }
    public bool IsPrimary { get; private set; }
    private ProductImage(string url, Product? product, bool isPrimary = false)
    {
        this.Url = url;
        this.IsPrimary = isPrimary;
        this.Product = product;      
    }
};