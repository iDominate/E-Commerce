using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Domain.Models.Orders.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints;


public sealed class CreateCategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/api/category/", HandleAsync);
    }

    public async Task<IResult> HandleAsync(CreateCategoryRequest request, ISender sender)
    {
        var result = await sender.Send(CreateCategoryRequest.ToCommand(request));

        if(!result.IsSuccess)
        {
            return Results.BadRequest(ApiResponse<IEnumerable<Error>>.Failure(result.Errors));
        }
        var response = ApiResponse<Category>.Successful(result.Value!);
        return Results.Created($"/api/categories/{result.Value!.Id}", response);

    }
}