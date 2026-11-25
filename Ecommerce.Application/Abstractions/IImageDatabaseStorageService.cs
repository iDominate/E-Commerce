using Ecommerce.Domain.Models.ProductAggregate.Entities;

namespace Ecommerce.Application.Abstractions;


public interface IImageDatabaseStorageService
{
    Task<ProductImage> StoreImageAsync(Guid ProductId,
    string imageName, 
    Stream fileStream,
    string contentType);

    Task RemoveImageAsync(Guid productId, string imageName);
    Task<IEnumerable<ProductImage>> GetImagesAsync(Guid ProductId);
}