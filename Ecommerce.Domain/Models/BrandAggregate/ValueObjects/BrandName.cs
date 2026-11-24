using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;
using OneOf;

namespace Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
public sealed record BrandName : ValueObject<BrandName, string>
{
    private static Regex Pattern = new(@"^([A-Z][a-z0-9'_-]+\s*)+[\w\s\d\d\s-,&']*$");
    private BrandName(string value): base(value)
    {        
    }
    public static DomainResult<BrandName> Create(string name)
    {
        if(!Pattern.IsMatch(name))
        {
            return DomainResult<BrandName>.Failure(
                new Error("BrandName.Invalid",
                    $"Brand name {name} is invalid.", ErrorType.Validation)
                );
        }
        return DomainResult<BrandName>.Success(new BrandName(name));
    }
}