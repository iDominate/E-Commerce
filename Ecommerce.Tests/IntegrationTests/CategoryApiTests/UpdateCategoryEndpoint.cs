
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Api.Endpoints.UpdateCategory;
using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;

namespace Ecommerce.Tests.IntegrationTests.CategoryApiTests;

public sealed class UpdateCategoryEndpoint : IntegrationTest
{
    public UpdateCategoryEndpoint(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task UpdateCategoryWithImporperInput_Shoud_ReturnError()
    {
        //Arrange
        var request = new UpdateCategoryRequest("", "");
        //Act
        var apiResult = await this._client.PutAsJsonAsync($"/api/category/{Guid.NewGuid()}", request);

        //Assert
        var response = await apiResult.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Error>>>();
        response.Should().NotBeNull();
        response.Errors.Should().HaveCountGreaterThan(0);
        response.Errors.Should().Contain(x => x.Code == "Name.Invalid");
        response.Errors.Should().Contain(x => x.Code == "Slug.Invalid");
        response.Errors.Should().Contain(x => x.ErrorType == ErrorType.Validation);
        apiResult.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
    [Fact]
    public async Task UpdateCategoryWithNameOrSlug_Shoud_ReturnError()
    {
        //Arrange
        var category = CategoriesFactory.Generate(1).First();
        category.Slug.Value.Replace("&", "and");
        await this._context.AddAsync(category);
        await this._context.SaveChangesAsync();
        var request = new UpdateCategoryRequest(category.Name.Value, category.Slug.Value);
        //Act
        var apiResult = await this._client.PutAsJsonAsync($"/api/category/{Guid.NewGuid()}", request);

        //Assert
        var response = await apiResult.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Error>>>();

        response.Should().NotBeNull();
        var existingCategory = this._context.Set<Category>().Where(c => c.Id == category.Id);    
        existingCategory.Should().NotBeNull();
        //cleanup
        this._context.Database.EnsureDeleted(); 
    }

    [Fact]
    public async Task UpdateCategoryWithDuplicateValues_Should_ReturnError()
    {
        //Arrange
        var category = CategoriesFactory.Generate(1).First();
        category.UpdateValues(Name.
        FromPersistence(category.Name.Value.Replace("&", "and")),
        Slug.FromPersistence(category.Slug.Value.Replace("&", "and")));        
        await this._context.AddAsync(category);
        await this._context.SaveChangesAsync();


        var request = new UpdateCategoryRequest(category.Name.Value, category.Slug.Value);
        //Act
        var apiResult = await this._client.PutAsJsonAsync($"/api/category/{category.Id}", request);

        //Assert
        var response = await apiResult.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Error>>>();

        apiResult.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        response!.Errors.Should().HaveCountGreaterThan(0);

        //cleanup
        this._context.Database.EnsureDeleted();
    }
}