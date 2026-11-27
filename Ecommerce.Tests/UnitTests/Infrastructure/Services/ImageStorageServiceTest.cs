using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Infrastructure.Services.ImageStorage;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ecommerce.Tests.UnitTests.Infrastructure.Services;

public sealed class ImageStorageServiceTest
{
    private readonly ImageDatabaseStorageService _service;
    private readonly Mock<IRepository<Product>> _repo;
    private readonly List<Product> _products;
    public ImageStorageServiceTest()
    {
        _repo = new Mock<IRepository<Product>>();
        var logger = new Mock<ILogger<ImageDatabaseStorageService>>();
        _service = new ImageDatabaseStorageService(_repo.Object, logger.Object);
        _products = [.. ProductFactory.Generate()];
    }

    [Fact]
    public async Task GetImagesAsync_ShouldReturnImages()
    {
        // Arrange
        var productId = _products.First().Id;
        _repo.Setup(c => c.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { _products.First() });
        // Acti
        var images = await _service.GetImagesAsync(productId);

        // Assert
        images.Should().NotBeNull();
        images.ToList().Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RemoveImageAsync_ShouldRemoveImage()
    {
        // Arrange
        var productId = _products.First().Id;
        var imageId = _products.First().Images.First().Id;
        _repo.Setup(c => c.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { _products.First() });

        // Act
        await _service.RemoveImageAsync(productId, imageId);

        // Assert
        var images = await _service.GetImagesAsync(productId);
        images.FirstOrDefault(i => i.Id == imageId).Should().BeNull();
    }

    [Fact]
    public async Task StoreImageAsync_ShouldStoreImage()
    {
        // Arrange
        var productId = _products.First().Id;
        var image = _products.First().Images.First();
        _repo.Setup(c => c.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { _products.First() });
        var imageStream = new MemoryStream(image.ImageContent);

        // Act
        await _service.StoreImageAsync(productId, image.ImageName, imageStream, image.ImageFileType);

        // Assert
        var images = await _service.GetImagesAsync(productId);
        images.FirstOrDefault(i => i.Id == image.Id).Should().NotBeNull();
    }
}