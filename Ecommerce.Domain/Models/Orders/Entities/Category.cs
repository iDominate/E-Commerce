namespace Ecommerce.Domain.Models.Orders.Entities;
/// <summary>
/// The category Aggerate root which implements <see cref="IAuditable"/>
/// </summary>
public sealed class Category : AggregateRoot, IAuditable
{
    private Category(Name name, Slug slug)
    {
        Name = name;
        Slug = slug;
    }

    public static Category Create(Name name, Slug slug) => new Category(name, slug);
    /// <summary>
    /// The name of the category
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    /// A unique hyphen seperated text for the specific category
    /// </summary>
    public Slug Slug { get; private set; }

    public DateTime CreatedAt {get; private set;}

    public DateTime UpdatedAt { get; private set; }
    

    public void UpdateValues(Name name, Slug slug)
    {
        this.Name = name;
        this.Slug = slug;
        this.UpdatedAt = DateTime.UtcNow;
    }
}