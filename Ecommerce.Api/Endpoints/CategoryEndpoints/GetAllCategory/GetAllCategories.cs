using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Application.CategoryCQRS.Queries.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints;

public class GetAllCategories : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/api/category/", HandleAsync);
    }
    public async Task<IResult>  HandleAsync([FromServices] ISender sender)
    {
       var result = await sender.Send(new GetAllCategoriesQuery());
       var response = ApiResponse<IEnumerable<CategoryDto>>.
       Successful(
        ((ApplicationResult<IEnumerable<CategoryDto>>)result)!.Value!);
        return Results.Ok(response);
    }
}