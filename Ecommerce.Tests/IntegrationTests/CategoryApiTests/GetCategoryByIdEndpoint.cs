using System.Net.Http.Json;
using Ecommerce.Api.Endpoints.Shared;
using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;

namespace Ecommerce.Tests.IntegrationTests.CategoryApiTests;

public sealed class GetCategoryByIdEndpoint : IntegrationTest
{
    public GetCategoryByIdEndpoint(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetCategoryByIdWithNonExistingId_Should_ReturnsFailure()
    {
        //Arrange
        //Act
        var result = await _client.GetAsync($"/api/category/{Guid.NewGuid()}"
        );

        var response = await result.Content.ReadFromJsonAsync<ApiResponse<Category>>();
        //Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        response!.Success.Should().BeFalse();
        response.Errors.Should().HaveCountGreaterThan(0); 
    }
}