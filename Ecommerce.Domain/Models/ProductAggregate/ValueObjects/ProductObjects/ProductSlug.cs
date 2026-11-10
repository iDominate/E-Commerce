using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;

/// <summary>
/// A Product slug class
/// </summary>
public sealed record ProductSlug : ValueObject<ProductSlug, string>
{
    private static IDomainResult InvalidSlug = 
    DomainResult<Error>.
    Failure("ProductSlug.Invalid", "Invalid product slug");
    private static readonly Regex Pattern =
        new(@"^([A-Za-z0-9]+)(\s[A-Za-z0-9]+)*$");
    public ProductSlug(string slug) : base(slug)
    {
    }
    /// <summary>
    /// Create an instance of ProductSlug
    /// </summary>
    /// <param name="slug">the slug</param>
    /// <returns>an <seealso cref="IDomainResult"/>/returns>
    public static IDomainResult Create(string slug)
    {
        if(!Pattern.IsMatch(slug))
        {
            return InvalidSlug;
        }
        return DomainResult<ProductSlug>.Success(new ProductSlug(slug));
    }
}