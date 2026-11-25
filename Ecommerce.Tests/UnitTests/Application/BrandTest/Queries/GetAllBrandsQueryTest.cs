using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Infrastructure.BasicImplementation;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Queries;

public sealed class GetAllBrandsQueryTest
{
    [Fact]
    public async Task GetAllBrandsQuery_ShouldReturnAllBrands()
    {
        // Arrange
        var repo = new Mock<IRepository<Brand>>();
        var spec = new Specification<Brand>();
        var brandsList = BrandsFactory.Generate(5);
        repo.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).Returns(Task.FromResult(brandsList));
        // Act
        var result = repo.Object.GetEntitiesAsync(spec);

        // Assert
        var brands = await result;
        brands.Should().NotBeNull();
        brands.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetAllBrandsQuery_ShouldReturnEmptyList_WhenNoBrandsExist()
    {
        // Arrange
        var repo = new Mock<IRepository<Brand>>();
        var spec = new Specification<Brand>();
        repo.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).ReturnsAsync(new List<Brand>());

        // Act
        var result = repo.Object.GetEntitiesAsync(spec);

        // Assert
        var brands = await result;
        brands.Should().NotBeNull();
        brands.Should().BeEmpty();
    }
}