using System.Runtime.CompilerServices;

public sealed class Result<T>
{
    private readonly T? _value;
    private Result(T value)
    {
        this._value = value;
        this.IsSuccess = true;        
    }

    private Result(Error[] errors)
    {
        this.Errors = errors;
        IsSuccess = false; 
    }

    public bool IsSuccess {get;}
    public bool isFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors {get;} = [];

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("cannot access the value");
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure(Error[] errors) => new Result<T>(errors);
    public static implicit operator Result<T>(T value) => new Result<T>(value);
}