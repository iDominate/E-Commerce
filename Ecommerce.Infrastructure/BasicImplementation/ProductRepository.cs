using Ecommerce.Domain.Models.ProductAggregate;

namespace Ecommerce.Infrastructure.BasicImplementation;

public class ProductRepository : Repository<Product>
{
    public ProductRepository(AppDbContext appDb) : base(appDb)
    {
    }
}