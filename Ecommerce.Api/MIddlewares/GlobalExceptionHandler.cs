using Ecommerce.Api.Endpoints.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Api.MIddlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
     Exception exception,
     CancellationToken cancellationToken)
    {
        if(exception is AppValidationException)
        {
            _logger.LogError(0,exception, "Validation Error occured with messsage: {message}",
             ((AppValidationException) exception).Message);
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(
            ApiResponse<IEnumerable<Error>>.Failure(((AppValidationException) exception).Errors));
        } else
        {
            _logger.LogError(1,exception, "Fatal Error: {message}", exception.Message);
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
            await httpContext.Response.WriteAsJsonAsync(ApiResponse<Error>.Failure([new Error("Unknown",
            exception.Message, ErrorType.Failure)]));
        }
        return true;
    }
}