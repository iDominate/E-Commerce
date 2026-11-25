namespace Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
public sealed record ImageDto(
    Guid ProductId,
    string ImageName,
    string FileType,
    byte[] ImageContent
)
{
  public static ImageDto ToDto(ProductImage image) =>
      new ImageDto(
          image.ProductId,
          image.ImageName,
          image.ImageFileType,
          image.ImageContent
      );


};