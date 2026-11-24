namespace Ecommerce.Domain.Models.CategoryAggregate;
/// <summary>
/// The category Aggerate root which implements <see cref="IAuditable"/>
/// </summary>
public sealed class Category : AggregateRoot, IAuditable
{
    private Category(CategoryName name, CategorySlug slug)
    {
        Name = name;
        Slug = slug;
        _products = new List<ProductAggregate.Product>();
    }

    private Category() {}

    public static Category Create(CategoryName name, CategorySlug slug) 
        => new Category(name, slug);
    /// <summary>
    /// The name of the category
    /// </summary>
    public CategoryName Name { get; private set; }

    /// <summary>
    /// A unique hyphen seperated text for the specific category
    /// </summary>
    public CategorySlug Slug { get; private set; }

private readonly List<ProductAggregate.Product> _products = new List<ProductAggregate.Product>()  ;

public IReadOnlyCollection<ProductAggregate.Product> Products
    => _products;
    public DateTime CreatedAt {get; init;}

    public DateTime UpdatedAt { get; set; }
    

    public void UpdateValues(CategoryName name, CategorySlug slug)
    {
        this.Name = name;
        this.Slug = slug;
        this.UpdatedAt = DateTime.UtcNow;
    }
    public void AddProduct(ProductAggregate.Product product)
    {
        this._products.Add(product);
        this.UpdatedAt = DateTime.UtcNow;
    }

}