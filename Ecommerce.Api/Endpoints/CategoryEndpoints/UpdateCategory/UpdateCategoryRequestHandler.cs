using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Api.Endpoints.UpdateCategory;
using Ecommerce.Application.CategoryCQRS.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints.UpdateCategory;

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
            return Results.BadRequest(ApiResponse<Error>.Failure(result.Errors!));
        }

        return Results.Ok(ApiResponse<CategoryDto>.
        Successful(((ApplicationResult<CategoryDto>)result).Value!));
    }
}