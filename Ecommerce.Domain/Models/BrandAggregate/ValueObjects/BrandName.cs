using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
public sealed record BrandName : ValueObject<BrandName, string>
{
    private static Regex Pattern = new(@"^([A-Z][a-z0-9',_-]+\s*)+[\w\s\d\d\s-,&']*$");
    public static DomainResult<BrandName> InvalidName => DomainResult<BrandName>.Failure(
        new Error("BrandName.Invalid", "Brand name is invalid.", ErrorType.Validation)
    );
    private BrandName(string value): base(value)
    {        
    }
    public static DomainResult<BrandName> Create(string name)
    {
        if(!Pattern.IsMatch(name))
        {
            return InvalidName;
        }
        return DomainResult<BrandName>.Success(new BrandName(name));
    }
}