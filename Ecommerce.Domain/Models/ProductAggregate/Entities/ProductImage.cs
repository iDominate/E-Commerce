using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.ProductAggregate.Entities;

public sealed class ProductImage : Entity
{
    public Guid ProductId { get; private set; }
    public Product? Product { get; private set; }
    public string Url  {get; private set;}
    public string  ImageName { get; private set; }
    public byte[] ImageContent { get; private set; }
    public string ImageFileType { get; private set; }
    public bool IsPrimary { get; private set; }
    private ProductImage()
    {
        
    }
    private ProductImage(Guid productId, string imageName, string url, byte[] imageContent, 
    string imageFileType, bool isPrimary = false
    )
    {
        this.ProductId = productId;
        this.Id = Guid.NewGuid();
        this.ImageName = imageName;
        this.ImageContent = imageContent;
        this.ImageFileType = imageFileType;
        this.Url = url;
        this.IsPrimary = isPrimary;
    }

    public static DomainResult<ProductImage> Create(
    Guid productId,
    string imageName,  
    byte[] imageContent, 
    string imageFileType, 
    bool isPrimary = true)
    {
        var url = String.Join(imageName, "-" ,DateTime.UtcNow.ToString());
        if(string.IsNullOrWhiteSpace(imageName) || string.IsNullOrWhiteSpace(url)
        || string.IsNullOrWhiteSpace(imageFileType))
        {
            return DomainResult<ProductImage>.Failure(
                new Error("ProductImage.InvalidInput", "Invalid image input.", ErrorType.Validation)
                );
        }
        var image = new 
        ProductImage(productId, imageName,
        url,
        imageContent,
        imageFileType,
        isPrimary);
        return DomainResult<ProductImage>.Success(image);
    }
};