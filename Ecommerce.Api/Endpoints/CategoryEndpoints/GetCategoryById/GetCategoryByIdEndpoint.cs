using System.Runtime.CompilerServices;
using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints.GetCategoryById;


public sealed class GetCategoryByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/api/category/{id}", HandleAsync);
    }

    public async Task<IResult> HandleAsync([AsParameters] GetCategoryByIdRequest request,[FromServices] ISender sender)
    {
        var result = await sender.Send(GetCategoryByIdRequest.ToQuery(request));
        if(!result.IsSuccess)
        {
            return Results.BadRequest(ApiResponse<Category>.Failure(result.Errors));
        }
        return Results.Ok(ApiResponse<Category>.Successful(result.Value!));

    }
}