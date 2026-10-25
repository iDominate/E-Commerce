using Ecommerce.Domain.Models.Orders.Entities;
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
    private DbSet<Category> Categories;
}