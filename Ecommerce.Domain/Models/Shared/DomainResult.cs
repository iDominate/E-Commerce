namespace Ecommerce.Domain.Models.Shared;


public sealed record DomainResult<T>
{
    public string Code {get;}
    public string? Message {get;} = null;
    public T Object {get; }
    public bool IsSuccess {get;}

    private DomainResult(T domainObject = default(T)!,
    string code = "Success",
    string? message = null,
    bool isSuccess = true
    )
    {
        this.IsSuccess = isSuccess;
        this.Message = message;
        this.Code = code;
        this.Object = domainObject;   
    }
    public static DomainResult<T> Success(T domainObject) => new DomainResult<T>(domainObject);

    public static DomainResult<T> Failure
    (string code, string message) => new(isSuccess: false ,
    code: code, message: message);

}