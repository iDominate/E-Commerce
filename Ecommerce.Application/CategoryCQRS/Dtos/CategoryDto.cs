using Ecommerce.Domain.Models.CategoryAggregate;

namespace Ecommerce.Application.CategoryCQRS.Dtos;

public sealed record CategoryDto(
    Guid Id,
    string Name,
    string Slug
)
{
    public static CategoryDto ToDto(Category category) =>
        new CategoryDto(
            category.Id,
            category.Name.Value,
            category.Slug.Value
        );
    public static IEnumerable<CategoryDto> ToDtoList(IEnumerable<Category> categories) =>
        categories.Select(ToDto);
};