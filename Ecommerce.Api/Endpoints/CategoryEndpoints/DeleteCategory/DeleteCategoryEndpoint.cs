using Ecommerce.Api.Endpoints.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints.DeleteCategory;

public sealed class DeleteCategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapDelete("/api/category/{id}", HandleAsync);
    }

    private async Task<IResult> HandleAsync([AsParameters] DeleteCategoryRequest request,
    [FromServices] ISender sender)
    {
        var result = await sender.Send(DeleteCategoryRequest.ToCommand(request));

        if(!result.IsSuccess)
        {
            return Results.BadRequest(ApiResponse<IEnumerable<Error>>.Failure(
                result.Errors));
        }
        return Results.NoContent();
    }
}