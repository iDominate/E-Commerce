using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;

namespace Ecommerce.Domain.Models.ProductAggregate;
public sealed class Product : AggregateRoot, IAuditable
{
    public ProductName Name {get; private set;}

    public ProductSlug Slug {get; private set;}

    public string Description {get; private set;}

    public ProductPrice? Price {get; private set;}
    public int Stock { get; private set; }
    public bool IsActive { get; private set; }
    public Brand? Brand { get; set; }
    private readonly List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images  => 
    _images;
    private readonly List<Review> _reviews = [];

    public ProductDiscount? Discount { get; private set; }
    public IReadOnlyCollection<Review> Reviews  => 
    _reviews;
    private readonly List<TransactionHistory> _transactionHistory = [];

    public IReadOnlyCollection<TransactionHistory> TransactionHistory  => 
    _transactionHistory;
    public DateTime CreatedAt { get ; init; }
    public DateTime UpdatedAt { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; private set; }

    Product(
    ProductName name,
    ProductSlug slug,
    string description,
    ProductPrice price,
    Guid brandId,
    Guid categoryId,
    ProductDiscount productDiscount){
    Name = name;
    Slug = slug;
    Description = description;
    Price = price;
    Stock = 0;
    IsActive = true;
    BrandId = brandId;
    CategoryId = categoryId;
    this.CreatedAt = DateTime.UtcNow;
    this.UpdatedAt = DateTime.UtcNow;
    this.Discount = productDiscount;
}

    public static Product Create(ProductName name, 
    ProductSlug slug, 
    string description,
    ProductPrice price, 
    Guid categoryId, 
    Guid brandId, 
    int stock, 
    bool isActive,
    ProductDiscount productDiscount = null!
    )
    {
        return new Product(name,
        slug,
        description,
        price,
        brandId,
        categoryId,
        productDiscount);
    }
    private Product()
    {
        
    }
    public void ChangePrice(ProductPrice price)
    {
        this.Price = price;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void AddImage(ProductImage image)
    {
        this._images.Add(image);
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveImage(Guid imageId)
    {
        this._images.RemoveAll(i => i.Id == imageId);
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

    public void AssignDiscount(ProductDiscount discount)
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

    public void AddReview(Review review)
    {
            this._reviews.Add(review);
            this.UpdatedAt = DateTime.UtcNow;    
    }
    public void ChangeName(ProductName name)
    {
        this.Name = name;   
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeSlug(ProductSlug slug)
    {
        this.Slug = slug;
        this.UpdatedAt = DateTime.UtcNow;
    }
}