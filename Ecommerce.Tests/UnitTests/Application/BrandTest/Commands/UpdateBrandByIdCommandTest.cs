using Ecommerce.Application.BrandCQRS.Commands.UpdateBrandByIdCommand;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Commands;

public sealed class UpdateBrandByIdCommandTest
{
    [Fact]
    public async Task UpdateBrandByIdCommand_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).Returns(
            Task.FromResult(BrandsFactory.Generate(1))
        );
        var command = new UpdateBrandByIdCommand(Guid.NewGuid(), "Valid Name");
        var handler = new UpdateBrandByIdCommandHandler(repository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}