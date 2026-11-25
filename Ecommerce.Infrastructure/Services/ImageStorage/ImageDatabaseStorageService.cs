using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services.ImageStorage;

public class ImageDatabaseStorageService : IImageDatabaseStorageService
{
    private ILogger<ImageDatabaseStorageService> _logger;
    private AppDbContext _db;

    public ImageDatabaseStorageService(AppDbContext db, ILogger<ImageDatabaseStorageService> logger)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IEnumerable<ProductImage>> GetImagesAsync(Guid productId)
    {
        var product = await _db.Products.FindAsync(productId);
        _logger.LogInformation("Fetched images for product {productId}", productId);
        return product!.Images;
    }

    public async Task RemoveImageAsync(Guid productId, string imageName)
    {
        var product = await _db.Products.FindAsync(productId);
        _logger.LogInformation("Removed image {imageName} for product with id {productId}",
        imageName, productId);
        var image = product!.Images.ToList().Find(i => i.ImageName == imageName);
    }

    public async Task<ProductImage> StoreImageAsync(Guid productId,
    string ImageName,
    Stream fileStream,
    string contentType)
    {
        var product = await _db.Products.FindAsync(productId);
        using var stream = new MemoryStream();
        await fileStream.CopyToAsync(stream);
        var fileBytes = stream.ToArray();
        var newImage = ProductImage.Create(productId, ImageName, fileBytes, contentType);
        _logger.LogInformation("store image {imageName} with content type {contentType} for product {productId}"
        , ImageName, contentType, productId);
        return newImage.Value!;
    }
}