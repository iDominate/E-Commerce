using Ecommerce.Domain.Models.CategoryAggregate;

namespace Ecommerce.Infrastructure.BasicImplementation;
public class CategoryRepository(AppDbContext appDb)
 : Repository<Category>(appDb);