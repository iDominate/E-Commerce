using Ecommerce.Application.BrandCQRS.Commands.CreateBrandCommand;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Commands;

public sealed class CreateBrandCommandTest
{
    [Fact]
    public async Task CreateBrandCommand_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        var command = new CreateBrandCommand("Valid Namei");
        var handler = new CreateBrandCommandHandler(repository.Object);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
