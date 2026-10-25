using Ecommerce.Domain.Models.Orders.Entities;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository(DbSet<Category> dbSet, AppDbContext appDb)
 : Repository<Category>(dbSet, appDb)
{
}