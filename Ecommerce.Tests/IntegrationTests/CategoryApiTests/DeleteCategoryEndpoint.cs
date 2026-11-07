using System.Net.Http.Json;
using Ecommerce.Api.Endpoints.CategoryEndpoints.DeleteCategory;
using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Tests.IntegrationTests.CategoryApiTests;


public sealed class DeleteCategoryEndpoint : IntegrationTest
{
    public DeleteCategoryEndpoint(CustomWebApplicationFactory factory) : base(factory)
    {
    }


    [Fact]
    public async Task DeleteCategoryWithImproperInput_Should_ReturnError()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        var response = await this._client.DeleteAsync($"/api/category/{guid}");
        //Assert
        System.Console.WriteLine(response.Content);
        var body = await response.
        Content.
        ReadFromJsonAsync<ApiResponse<IEnumerable<Error>>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);        
        body!.Errors!.Count().Should().BeGreaterThan(0);
    }
    [Fact]
    public async Task DeleteCategoryWithProperId_Should_ReturnSuccess()
    {
        //Arrange
        var category = CategoriesFactory.Generate().First();

        await this._context.AddAsync(category);
        await this._context.SaveChangesAsync();

        //Act
        var response = await this._client.DeleteAsync($"/api/category/{category.Id}");
        var content = response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();

        //Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        (await this._context.Set<Category>().ToListAsync()).Count.Should().Be(0);
    }
}
