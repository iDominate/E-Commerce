using Ecommerce.Application.BrandCQRS.Commands.UpdateBrandByIdCommand;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Validators;

public sealed class UpdateBrandValidatorByIdTest
{
    [Fact]
    public async Task UpdateBrandCommandWithInvalidIdadnImproperName_Should_ReturnValidationError()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).ReturnsAsync(
            Array.Empty<Brand>()
        );
        var command = new UpdateBrandByIdCommand(Guid.Empty, "new brand");
        var validator = new UpdateBrandByIdCommandValidator(repository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors.Should().Contain(e => e.ErrorCode == "Brand.NotFound");
        result.Errors.Should().Contain(e => e.ErrorCode == BrandName.InvalidName.Error!.Code);
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateBrandCommandWithValidIdAndProperName_Should_PassValidation()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).ReturnsAsync(
            BrandsFactory.Generate(10)
        );
        var command = new UpdateBrandByIdCommand(Guid.NewGuid(), "ValidBrandName");
        var validator = new UpdateBrandByIdCommandValidator(repository.Object);

        // Act
        var result = await validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}