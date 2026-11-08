using Ecommerce.Api.Endpoints.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Api.MIddlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is AppValidationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(
            ApiResponse<IEnumerable<Error>>.Failure(((AppValidationException) exception).Errors));
        } else
        {
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
            await httpContext.Response.WriteAsJsonAsync(ApiResponse<string>.Failure([new Error("Unknown",
            exception.Message, ErrorType.Failure)]));
        }
        return true;
    }
}