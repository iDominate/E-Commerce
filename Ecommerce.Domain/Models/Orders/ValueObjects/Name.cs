using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

/// <summary>
/// A name <see cref="ValueObject<string>"/>
/// </summary>
public sealed record Name : ValueObjet<Name, string>
{
    public static DomainResult<Name> InvalidName = DomainResult<Name>.
    Failure("Name.Invalid", "invalid name format");
    private static readonly Regex Pattern =
        new(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$");
    private Name(string value) : base(value)
    {    
    }
    public static DomainResult<Name> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return InvalidName;
        }
        if(!Pattern.IsMatch(value))
        {
            return InvalidName;
        }
        return DomainResult<Name>.Success(new Name(value));           
    }

    public static Name FromPersistence(string value)
    {
            return new Name(value);
    }
}