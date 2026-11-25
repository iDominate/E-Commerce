using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Application.CategoryCQRS.Dtos;
using MediatR;

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
            return Results.BadRequest(ApiResponse<IEnumerable<Error>>.Failure(result.Errors!));
        }
        var response = ApiResponse<CategoryDto>.Successful(((ApplicationResult<CategoryDto>)result).Value!);
        return Results.Created($"/api/category/{((ApplicationResult<CategoryDto>)result).Value!.Id}", response);

    }
}