using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;

namespace Ecommerce.Infrastructure.BasicImplementation;

public class BrandRepository(AppDbContext appDb) : Repository<Brand>(appDb);