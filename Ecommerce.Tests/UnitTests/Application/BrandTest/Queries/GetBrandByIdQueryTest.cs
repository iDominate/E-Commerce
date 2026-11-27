using Ecommerce.Application.BrandCQRS.Queries.GetBrandById;
using Ecommerce.Tests.IntegrationTests.Shared;
using Moq;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using FluentAssertions;
using Ecommerce.Application.BrandCQRS.Dtos;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Queries;

public sealed class GetBrandByIdQueryTest
{
    [Fact]
    public async Task GetBrandByIdQuery_Should_ReturnBrand()
    {
        // Arrange
        var repo = new Mock<IRepository<Brand>>();
        var brand = BrandsFactory.Generate(1).First();
        repo.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Brand>>()))
            .ReturnsAsync(new List<Brand> { brand });
        var query = new GetBrandByIdQuery(brand.Id);
        var handler = new GetBrandByIdQueryHandler(repo.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        ((ApplicationResult<BrandDto>)result).Value.Should().BeEquivalentTo(BrandDto.ToDto(brand));
    }
}