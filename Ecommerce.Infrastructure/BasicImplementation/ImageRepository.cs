
using Ecommerce.Domain.Models.ProductAggregate.Entities;

namespace Ecommerce.Infrastructure.BasicImplementation;

public sealed class ImageRepository(AppDbContext appDb) : Repository<ProductImage>(appDb)
{
}