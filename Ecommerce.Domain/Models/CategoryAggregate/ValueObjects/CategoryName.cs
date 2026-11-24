using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

/// <summary>
/// A name <see cref="ValueObject<string>"/>
/// </summary>
public sealed record CategoryName : ValueObject<CategoryName, string>
{
    public static IDomainResult<CategoryName> InvalidName = DomainResult<CategoryName>.
    Failure(new("CategoryName.Invalid", 
    "Invalid category name",
    ErrorType.Validation));
    private static readonly Regex Pattern =
        new(@"^([\p{Lu}]+[\p{Ll}\p{N}\s,&-]+)+$");
    private CategoryName(string value) : base(value)
    {    
    }
    public static IDomainResult<CategoryName> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return InvalidName;
        }
        if(!Pattern.IsMatch(value))
        {
            return InvalidName;
        }
        return DomainResult<CategoryName>.Success(new CategoryName(value));           
    }

    public static CategoryName FromPersistence(string value)
    {
            return new CategoryName(value);
    }
}