public sealed class Category : AggregateRoot, IAuditable
{
    public Category(Name name, Slug slug)
    {
        Name = name;
        Slug = slug;
    }

    public Name Name { get; private set; }
    public Slug Slug { get; private set; }

    public DateTime CreatedAt {get; private set;}

    public DateTime UpdatedAt { get; private set; }
    

    public void UpdateName(Name name) => this.Name = name;
    public void UpdateSlug(Slug slug) => this.Slug = Slug;
}