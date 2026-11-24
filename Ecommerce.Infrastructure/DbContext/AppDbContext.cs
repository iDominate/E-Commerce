using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {   
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entity in ChangeTracker.Entries<IAuditable>())
        {
            if(entity.State == EntityState.Added)
            {
                entity.Property(nameof(IAuditable.CreatedAt))
                .CurrentValue = DateTime.UtcNow;

                entity.Property(nameof(IAuditable.UpdatedAt))
                .CurrentValue = DateTime.UtcNow;
            }
            if(entity.State == EntityState.Modified)
            {
                entity.Property(nameof(IAuditable.UpdatedAt))
                .CurrentValue = DateTime.UtcNow;
            }
        }
            return base.SaveChangesAsync(cancellationToken);
        
    }
    public DbSet<Category> Categories {get; private set;}
    public DbSet<Brand> BrandNames {get; private set;}
    public DbSet<Product> Products {get; private set;}
    public DbSet<TransactionHistory> Transactions {get; private set;}
    public DbSet<ProductImage> Images {get; private set;}
}