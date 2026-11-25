using Ecommerce.Application.Repositories.Contracts;
using Moq;
using Ecommerce.Application.BrandCQRS.Queries.GetBrandById;
using Ecommerce.Domain.Models.BrandAggregate;
using FluentAssertions;
using Ecommerce.Tests.IntegrationTests.Shared;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Validators;

public sealed class GetBrandByIdValidatorTest
{
    [Fact]
    public void GetBrandById_Should_HaveError_WhenBrandNotFound()
    {
        // Arrange
        var brandRepository = new Mock<IRepository<Brand>>();
        brandRepository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Brand>>()))
                      .ReturnsAsync(new List<Brand>());
        var validator = new GetBrandByIdQueryValidator(brandRepository.Object);
        var query = new GetBrandByIdQuery(Guid.NewGuid());

        // Act
        var result = validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "BrandId" && e.ErrorCode == "Brand.NotFound");
    }

    [Fact]
    public void GetBrandById_Should_NotHaveError_WhenBrandExists()
    {
        // Arrange
        var brandRepository = new Mock<IRepository<Brand>>();
        var brand = BrandsFactory.Generate(1).First();
        brandRepository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Brand>>()))
                      .ReturnsAsync(new List<Brand> { brand });
        var validator = new GetBrandByIdQueryValidator(brandRepository.Object);
        var query = new GetBrandByIdQuery(brand.Id);

        // Act
        var result = validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}