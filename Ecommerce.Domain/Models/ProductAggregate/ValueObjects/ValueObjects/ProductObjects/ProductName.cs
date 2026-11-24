using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;

/// <summary>
/// A Product name ValueObject
/// </summary>
public sealed record ProductName : ValueObject<ProductName, string>
{
    private static readonly Regex Pattern =
        new(@"^([A-Z][a-z0-9\s.,+_&-]+)+$");
    private ProductName(string value) : base(value)
    {
    }
    /// <summary>
    /// A method to instanitiate a ProductNam instance
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IDomainResult<ProductName> Create(string name)
    {
        if(!Pattern.IsMatch(name))
        {
            return DomainResult<ProductName>.
            Failure(new Error("ProductName.Invalid", "invalid product name input", ErrorType.Validation));
        }
        return DomainResult<ProductName>.Success(new ProductName(name));
    }
}