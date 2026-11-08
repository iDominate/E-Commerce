
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

/// <summary>
/// A slug <see cref="ValueObject<string>"/>
/// </summary>
public sealed record Slug : ValueObjet<Slug, string>
{
    public static DomainResult<Slug> InvalidSlug =
     DomainResult<Slug>.Failure("Slug.Invalid", "Invalid Slug Format");

    private static readonly Regex Pattern =
    new(@"^[a-z0-9]+(-[a-z0-9&]+)*$");
    private Slug(string value) : base(value)
    {
    }

    public static DomainResult<Slug> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return InvalidSlug;
        }
        if(!Pattern.IsMatch(value))
        {
            return InvalidSlug;
        }
        return DomainResult<Slug>.Success(new (value));
    }
    public static Slug FromPersistence(string value)
    {
        return new Slug(value);
    }
}