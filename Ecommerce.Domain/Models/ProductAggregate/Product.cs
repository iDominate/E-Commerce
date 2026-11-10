using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;

namespace Ecommerce.Domain.Models.ProductAggregate;


/// <summary>
/// A product aggregate root
/// </summary>
public sealed class Product : AggregateRoot, IAuditable
{
    public ProductName Name {get; private set;}

    public ProductSlug Slug {get; private set;}

    public string Description {get; private set;}

    public Money? Price {get; private set;}
    public int Stock { get; private set; }
    public bool IsActive { get; private set; }
    public Category? Category {get; private set;}
    public ProductBrand? Brand { get; set; }
    public List<ProductImage> Images { get; private set; } = 
    new List<ProductImage>();
    public IProductDiscount? Discount { get; private set; }
    public List<ProductReview> Reviews { get; private set; } =
    new List<ProductReview>();

    public List<ProductTransactionHistory> TransactionHistory { get; private set; } =
    new List<ProductTransactionHistory>();
    public DateTime CreatedAt { get ; set; }
    public DateTime UpdatedAt { get; set; }

    private Product(
    ProductName name,
    ProductSlug slug,
    string description,
    Money price,
    Category category)
{
    Name = name;
    Slug = slug;
    Description = description;
    Price = price;
    Category = category;
    Stock = 0;
    IsActive = true;
    this.UpdatedAt = DateTime.UtcNow;
}
    public void ChangePrice(Money price)
    {
        this.Price = price;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void AddImage(ProductImage image)
    {
        this.Images.Add(image);
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveImage(Guid imageId)
    {
        this.Images.Where(i => i.Id == imageId);
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        this.IsActive = true;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        this.IsActive = false;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void AssignDiscount(IProductDiscount discount)
    {
        this.Discount = discount;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveDiscount()
    {
        this.Discount = null;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void IncreaseStock(int quantity)
    {
        this.Stock += quantity;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void DecreaseStock(int quantity)
    {
        this.Stock -= quantity;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void AddReview(ProductReview review)
    {
            this.Reviews.Add(review);
            this.UpdatedAt = DateTime.UtcNow;    
    }
}