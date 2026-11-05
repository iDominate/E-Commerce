using System.Runtime.CompilerServices;
using Ecommerce.Api.Endpoints.CategoryEndpoints.Dtos;
using Ecommerce.Api.Endpoints.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints;

public class GetAllCategories : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/api/category/", HandleAsync);
    }
    public async Task<IResult>  HandleAsync(HttpContext context, ISender sender)
    {
       var result = await sender.Send(new GetAllCategoriesCommand());
       var response = ApiResponse<IEnumerable<CategoryResponseDto>>.
       Successful(
        CategoryResponseDto.ToMultipleObjectsResponse(result.Value)
        );
        return Results.Ok(response);
    }
}