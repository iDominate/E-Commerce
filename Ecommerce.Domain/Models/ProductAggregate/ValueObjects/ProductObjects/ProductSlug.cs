using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;

/// <summary>
/// A Product slug class
/// </summary>
public sealed record ProductSlug : ValueObject<ProductSlug, string>
{
    private static IDomainResult<ProductSlug> InvalidSlug = 
    DomainResult<ProductSlug>.
    Failure(
        new Error("ProductSlug.Invalid", "Invalid product slug.", ErrorType.Validation)!
        );
    private static readonly Regex Pattern =
        new(@"^[\p{Ll}\p{N}-]+$");
    public ProductSlug(string value) : base(value)
    {
    }
    /// <summary>
    /// Create an instance of ProductSlug
    /// </summary>
    /// <param name="slug">the slug</param>
    /// <returns>an <seealso cref="IDomainResult"/>/returns>
    public static IDomainResult<ProductSlug> Create(string slug)
    {
        if(!Pattern.IsMatch(slug))
        {
            return InvalidSlug;
        }
        return DomainResult<ProductSlug>.Success(new ProductSlug(slug));
    }
}