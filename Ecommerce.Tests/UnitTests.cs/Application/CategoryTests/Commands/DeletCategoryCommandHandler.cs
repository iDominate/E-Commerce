using System.Linq.Expressions;
using Ecommerce.Application.Category.Commands.DeleteCategory;
using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Application.CategoryTests.Commands;

public sealed class DeleteCategoryCommandTest : GenericRepositoryProvider<Category>
{

    [Fact]
    public async Task DeleteCategory_Should_ReturnFailureUponNonExitantGuid()
    {
        //Arrange
        this._mockRepo.Setup(m => m.RemoveEntityAsync(It.IsAny<Specification<Category>>()))
        .ReturnsAsync(false);
        var handler = new DeleteCategoryCommandHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new 
        DeleteCategoryCommand(Guid.Empty)
        ,this._cancellationTokenSource.Token);

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].ErrorType.Should().Be(ErrorType.NotFound);   
    }

    [Fact]
    public async Task DeleteCategory_Should_ReturnSuccess()
    {
        //Arrange
        var handler = new DeleteCategoryCommandHandler(this._mockRepo.Object);
        this._mockRepo.Setup(m => m.RemoveEntityAsync(
            It.IsAny<ISpecification<Category>>()))
        .ReturnsAsync(true);
        //Act
        var result = await handler.Handle(new 
        DeleteCategoryCommand(Guid.Empty)
        ,this._cancellationTokenSource.Token);

        //Assert
        result.Errors.Should().HaveCount(0);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}