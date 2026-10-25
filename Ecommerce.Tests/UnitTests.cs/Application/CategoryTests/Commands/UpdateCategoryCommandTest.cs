using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Application.CategoryTests.Commands;


public sealed class UpdateCategoryCommandHanlder : GenericRepositoryProvider<Category>
{
    [Fact]
    public async Task UpdateCategory_Should_ReturnFailureUponNonExitantGuid()
    {
        //Arrange
        this._mockRepo.Setup(m => m.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
        .ReturnsAsync(new List<Category>());
        var handler = new UpdateCategoryCommandHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new 
        UpdateCategoryCommand(Guid.Empty, new("Test 1"), new("test-1"))
        ,this._cancellationTokenSource.Token);

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].ErrorType.Should().Be(ErrorType.NotFound);   
    }

    [Fact]
    public async Task UpdateCategory_Should_ReturnSuccess()
    {
        //Arrange
        this._mockRepo.Setup(m => m.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
        .ReturnsAsync(new List<Category>{new(new("Test 1"), new("test-1"))});
        var handler = new UpdateCategoryCommandHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new 
        UpdateCategoryCommand(Guid.Empty, new("Test 1"), new("test-1"))
        ,this._cancellationTokenSource.Token);

        //Assert
        result.Errors.Should().HaveCount(0);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}