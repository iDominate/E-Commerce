using Ecommerce.Domain.Models.Orders.Entities;
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
            value =>  Name.FromPersistence(value)
        )
        .IsRequired()
        .HasMaxLength(32);

        builder.HasIndex(p => p.Name)
        .IsUnique();

        builder.Property(p => p.Slug)
        .HasConversion(slug => slug.Value,
        value =>  Slug.FromPersistence(value))
        .IsRequired();
    }
}