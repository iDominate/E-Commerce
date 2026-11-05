using Ecommerce.Api.Endpoints.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Api.MIddlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is ValidationException)
        {
            await httpContext.Response.WriteAsJsonAsync(
            ApiResponse<Error>.Failure(((ValidationException) exception).Errors.Select(e => new Error(
                e.ErrorCode, e.ErrorMessage, ErrorType.Validation))));
        } else
        {
            await httpContext.Response.WriteAsJsonAsync(ApiResponse<string>.Failure([new Error("Unknown",
            exception.Message, ErrorType.Failure)]));
        }
        return true;
    }
}