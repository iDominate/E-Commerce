using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;

/// <summary>
/// A Product name ValueObject
/// </summary>
public sealed record ProductName : ValueObject<ProductName, string>
{
    private static readonly Regex Pattern =
        new(@"^([A-Za-z0-9]+)(\s[A-Za-z0-9]+)*$");
    private ProductName(string name) : base(name)
    {
    }
    /// <summary>
    /// A method to instanitiate a ProductNam instance
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IDomainResult Create(string name)
    {
        if(!Pattern.IsMatch(name))
        {
            return DomainResult<Error>.
            Failure("ProductName.Invalid", "invalid product name input");
        }
        return DomainResult<ProductName>.Success(new ProductName(name));
    }
}