using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.UpdateCategory;

public sealed class UpdateCategoryRequestEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPut("/api/category/{id}", HandleAsync);
    }

    private async Task<IResult> HandleAsync([AsParameters] Guid id,
    [FromBody] UpdateCategoryRequest request,
    [FromServices] ISender sender)
    {
        var result = await sender.Send(UpdateCategoryRequest.ToCommand(id, request));

        if(!result.IsSuccess)
        {
            return Results.BadRequest(ApiResponse<Error>.Failure(result.Errors));
        }

        return Results.Ok(ApiResponse<Category>.Successful(result.Value!));
    }
}