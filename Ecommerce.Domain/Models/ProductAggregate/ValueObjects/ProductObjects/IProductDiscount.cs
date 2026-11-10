namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;

public abstract record IProductDiscount
{
    public Guid Id { get; set; }
    public Name DiscountName { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
}