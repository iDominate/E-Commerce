using Ecommerce.Domain.Models.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration
    : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name)
        .HasConversion(
            name => name.Value,
            value =>  CategoryName.FromPersistence(value)
        )
        .IsRequired()
        .HasMaxLength(32);

        builder.HasIndex(p => p.Name)
        .IsUnique();

        builder.Property(p => p.Slug)
        .HasConversion(slug => slug.Value,
        value =>  CategorySlug.FromPersistence(value))
        .IsRequired();
        
        builder.HasMany(c => c.Products)
        .WithOne()
        .HasForeignKey(p => p.CategoryId)
        .HasPrincipalKey(c => c.Id);

        builder.Navigation(c => c.Products)
        .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}