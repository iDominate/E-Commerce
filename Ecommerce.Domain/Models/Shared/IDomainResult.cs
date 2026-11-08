namespace Ecommerce.Domain.Models.Shared;

public interface IDomainResult
{
    public string Code {get;}
    public string? Message {get;}

    public bool IsSuccess {get;}
}