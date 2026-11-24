
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

/// <summary>
/// A slug <see cref="ValueObject<string>"/>
/// </summary>
public sealed record CategorySlug : ValueObject<CategorySlug, string>
{
    public static DomainResult<CategorySlug> InvalidSlug =
     DomainResult<CategorySlug>.Failure(new Error("CategorySlug.Invalid",
     "Invalid slug format.", ErrorType.Validation));

    private static readonly Regex Pattern =
    new(@"^[a-z0-9_?]+(-[a-z0-9_?]+)*$");
    private CategorySlug(string value) : base(value)
    {
    }

    public static DomainResult<CategorySlug> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return InvalidSlug;
        }
        if(!Pattern.IsMatch(value))
        {
            return InvalidSlug;
        }
        return DomainResult<CategorySlug>.Success(new (ToSlugFormat(value)));
    }
    public static CategorySlug FromPersistence(string value)
    {
        return new CategorySlug(ToSlugFormat(value));
    }
    public static string ToSlugFormat(string value)
    {
        return value
        .ToLowerInvariant()
        .Replace("&", "and")
        .Replace(",", "")
        .Replace(" ", "-").Replace("?", "")
        .Replace("_", "-");
    }
}