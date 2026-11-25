    using System.Runtime.CompilerServices;
    using Ecommerce.Application.Shared;

    public class ApplicationResult<T> : IApplicationResult
    {
        public T? Value {get; init;}

        public ApplicationResult(T? value = default(T), bool isSuccess = true,
        IEnumerable<Error> errors = null!)
        {
            Value = value;
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess {get; init;}
        public IEnumerable<Error> Errors {get; init;} = [];

        public static ApplicationResult<T> Success(T value) => new ApplicationResult<T>(value);
        public static ApplicationResult<T> Failure(params Error[] errors) => new ApplicationResult<T>(isSuccess: false,

        errors: errors);
        public static implicit operator ApplicationResult<T>(T value) => new ApplicationResult<T>(value: value);
    }