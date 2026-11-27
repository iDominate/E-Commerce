using Ecommerce.Application.BrandCQRS.Commands.DeleteBrandByIdCommand;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Commands;

public sealed class DeleteBrandByIdCommandTest
{
    [Fact]
    public async Task DeleteBrandByIdCommand_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        repository.Setup(r => r.RemoveEntityAsync(It.IsAny<ISpecification<Brand>>())).ReturnsAsync(true);
        var command = new DeleteBrandByIdCommand(Guid.NewGuid());
        var handler = new DeleteBrandByIdCommandHandler(repository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNull();
    }

}