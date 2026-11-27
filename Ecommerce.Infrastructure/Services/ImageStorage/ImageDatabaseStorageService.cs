using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services.ImageStorage;

public class ImageDatabaseStorageService : IImageDatabaseStorageService
{
    private ILogger<ImageDatabaseStorageService> _logger;
    private IRepository<Product> _repo;

    public ImageDatabaseStorageService(IRepository<Product> db, ILogger<ImageDatabaseStorageService> logger)
    {
        _logger = logger;
        _repo = db;
    }

    public async Task<IEnumerable<ProductImage>> GetImagesAsync(Guid productId)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == productId);
        var product = (await _repo.GetEntitiesAsync(spec)).FirstOrDefault();
        _logger.LogInformation("Fetched images for product {productId}", productId);
        return product!.Images;
    }

    public async Task RemoveImageAsync(Guid productId, Guid imageId)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == productId);
        var product = (await _repo.GetEntitiesAsync(spec)).FirstOrDefault();
        _logger.LogInformation("Removed image {imageId} for product with id {productId}",
        imageId, productId);
        product!.RemoveImage(imageId);
    }

    public async Task<ProductImage> StoreImageAsync(Guid productId,
    string ImageName,
    Stream fileStream,
    string contentType)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == productId);
        var product = (await _repo.GetEntitiesAsync(spec)).FirstOrDefault();
        using var stream = new MemoryStream();
        await fileStream.CopyToAsync(stream);
        var fileBytes = stream.ToArray();
        var newImage = ProductImage.Create(productId, ImageName, fileBytes, contentType);
        product!.AddImage(newImage.Value!);
        _logger.LogInformation("store image {imageName} with content type {contentType} for product {productId}"
        , ImageName, contentType, productId);
        return newImage.Value!;
    }
}