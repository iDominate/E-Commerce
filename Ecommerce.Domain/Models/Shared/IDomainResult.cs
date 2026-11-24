namespace Ecommerce.Domain.Models.Shared;

public interface IDomainResult<T>
{
    public T? Value { get; init; }
    public Error? Error { get; init; }
    public bool IsSuccess {get; init;}
}