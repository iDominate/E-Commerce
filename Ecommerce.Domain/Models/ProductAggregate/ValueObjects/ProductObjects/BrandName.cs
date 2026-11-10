using System.Text.RegularExpressions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;
public sealed record BrandName : ValueObject<BrandName, string>
{
    private static Regex Pattern = new(@"([a-z-A-z0-9_-]+\s*)+");
    public string Value { get; private set; }
    private BrandName(string name): base(name)
    {        
    }
    public static IDomainResult Create(string name)
    {
        if(!Pattern.IsMatch(name))
        {
            return DomainResult<Error>.Failure("BrandName.InvalidName", "Invalid name input");
        }
        return DomainResult<BrandName>.Success(new BrandName(name));
    }
}