public sealed record Name : ValueObjet<string>
{
    public Name(string value) : base(value)
    {    
    }
    public override ValueObjet<string> CreateInstance(string value)
    {
        return new Name(value);           
    }
}