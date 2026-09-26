public sealed record Slug : ValueObjet<string>
{
    public Slug(string value) : base(value)
    {
    }

    public override ValueObjet<string> CreateInstance(string value)
    {
        return new Slug(value);
    }
}