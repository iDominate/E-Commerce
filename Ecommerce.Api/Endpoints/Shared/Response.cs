namespace Ecommerce.Api.Endpoints.Shared;

public sealed record ApiResponse<T>(bool Success, T? Data, IEnumerable<Error>? Errors)
{
    public static ApiResponse<T> Successful(T data) => new ApiResponse<T>(true, data, null);
    public static ApiResponse<T> Failure(IEnumerable<Error> errors) => new ApiResponse<T>(false, default(T), errors);   
}