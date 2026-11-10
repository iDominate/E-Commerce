namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;

public sealed record PercentDiscount : IProductDiscount
{
    public decimal Percent { get; private set; }    
}