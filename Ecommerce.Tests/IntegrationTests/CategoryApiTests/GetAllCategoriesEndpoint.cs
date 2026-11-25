using System.Net.Http.Json;
using Bogus;
using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Ecommerce.Tests.IntegrationTests.CategoryApiTests;


public sealed class GetAllCategoriesEndpoint : IntegrationTest
{
    public GetAllCategoriesEndpoint(CustomWebApplicationFactory factory) : base(factory)
    {    
    }

    [Fact]
    public async Task GetAllCategories_Should_ReturnAllItems()
    {
        //Arrange
        var categories = CategoriesFactory.Generate();
        this._context.AddRange(categories);
        this._context.SaveChanges();

        //Act
        var result = await _client.GetAsync("/api/category/");


        //Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var objects = await result.Content.
        ReadFromJsonAsync<ApiResponse<IEnumerable<CategoryDto>>>();
        objects.Should().NotBeNull();
        objects.Data!.ToList().Should().HaveCount(10);

        //Cleanup
        this._context.RemoveRange(categories);
        this._context.SaveChanges();
    }
}