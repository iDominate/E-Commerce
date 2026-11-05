using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;

namespace Ecommerce.Tests.IntegrationTests.CategoryApiTests;

public sealed class CreateCategoryEndpoint : IntegrationTest
{
    public CreateCategoryEndpoint(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateCategoryWithImporperInput_Shoud_ReturnError()
    {
        //Arrange
        var request = new CreateCategoryRequest("", "");
        //Act
        var apiResult = await this._client.PostAsJsonAsync("/api/category/", request);

        //Assert
        var response = await apiResult.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Error>>>();
        response.Should().NotBeNull();
        response.Errors.Should().HaveCountGreaterThan(0);
        response.Errors.ToList()[0].Code.Should().Be("Name.Invalid");
        response.Errors.ToList()[1].Code.Should().Be("Slug.Invalid");
        response.Errors.ToList()[0].ErrorType.Should().Be(ErrorType.Validation);
        apiResult.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
    [Fact]
    public async Task CreateCategoryWithNameOrSlug_Shoud_ReturnError()
    {
        //Arrange
        var category = CategoriesFactory.Generate(1).First();
        category.Slug.Value.Replace("&", "and");
        await this._context.AddAsync(category);
        await this._context.SaveChangesAsync();
        var request = new CreateCategoryRequest(category.Name.Value, category.Slug.Value);
        //Act
        var apiResult = await this._client.PostAsJsonAsync("/api/category", request);

        //Assert
        var response = await apiResult.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Error>>>();

        response.Should().NotBeNull();
        var existingCategory = this._context.Set<Category>().Where(c => c.Id == category.Id);    
        existingCategory.Should().NotBeNull();
        //cleanup
        this._context.Database.EnsureDeleted(); 
    }
}