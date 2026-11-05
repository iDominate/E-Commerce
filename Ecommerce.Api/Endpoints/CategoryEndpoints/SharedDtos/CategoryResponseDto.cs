using Ecommerce.Domain.Models.Orders.Entities;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints.Dtos;

public sealed record CategoryResponseDto(Guid Id, string Name, string Slug)
{
    public static CategoryResponseDto ToSingleObjectReponse(Category category)
    {
        return new(category.Id, category.Name.Value, category.Slug.Value);   
    }
    public static IEnumerable<CategoryResponseDto> ToMultipleObjectsResponse(IEnumerable<Category> categories)
    {
        return categories.Select(c => new CategoryResponseDto(c.Id, c.Name.Value, c.Slug.Value));
    }
};