namespace Ecommerce.Domain.Models.Orders.Entities;
/// <summary>
/// The category Aggerate root which implements <see cref="IAuditable"/>
/// </summary>
public sealed class Category : AggregateRoot, IAuditable
{
    public Category(Name name, Slug slug)
    {
        Name = name;
        Slug = slug;
    }
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
    

    public void UpdateName(Name name) => this.Name = name;
    public void UpdateSlug(Slug slug) => this.Slug = Slug;
}