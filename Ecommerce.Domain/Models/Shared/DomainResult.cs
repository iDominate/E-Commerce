namespace Ecommerce.Domain.Models.Shared;


public sealed class DomainResult<T> : IDomainResult<T>
{
    public T? Value {get; init;}
    public bool IsSuccess {get; init;}

    public Error? Error {get; init;}
    private DomainResult(T? domainObject = default, bool isSuccess = true,  Error? error = null)
    {
        Error = error;
        IsSuccess = isSuccess;
        Value = domainObject;
    }

    
    public static DomainResult<T> Success(T domainObject) => new DomainResult<T>(domainObject);

    public static DomainResult<T> Failure
    (Error error) => new DomainResult<T>(default!, false, error
    );

}