using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.BrandAggregate.ValueObjects;

public sealed record BrandSlug : ValueObject<BrandSlug, string>
{
    public static IDomainResult<BrandSlug> InvalidSlug => DomainResult<BrandSlug>.Failure(new Error("BrandSlug.Invalid",
            "Invalid brand slug format.", 
            ErrorType.Validation));
    public BrandSlug(string slug) : base(Normalize(slug))
    {
        
    }

    public static string Normalize(string slug)
    {
        return slug.ToLowerInvariant().
        Replace("'", "").
        Replace(" - ", "-").
        Replace(" ", "-").
        Replace("_", "-").
        Replace("&", "and").
        Replace(",", "")
        ;
    }

    public static IDomainResult<BrandSlug> Create(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return InvalidSlug;
        }
        var normalizedSlug = Normalize(slug);
        return DomainResult<BrandSlug>.Success(new BrandSlug(normalizedSlug));
    }
}