using Ecommerce.Application.BrandCQRS.Commands.CreateBrandCommand;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Validators;

public sealed class CreateBrandValidatorTest
{
    [Fact]
    public async Task CreateBrandCommandWithImproperName_Should_ReturnValidationError()
    {
        // Arrange
        var command = new CreateBrandCommand("");
        var validator = new CreateBrandCommandValidator();
        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().
        Which.ErrorCode.Should().Be(BrandName.InvalidName.Error!.Code);
    }
    [Fact]
    public async Task CreateBrandCommandWithValidName_Should_PassValidation()
    {
        // Arrange
        var command = new CreateBrandCommand("ValidBrandName");
        var validator = new CreateBrandCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

}