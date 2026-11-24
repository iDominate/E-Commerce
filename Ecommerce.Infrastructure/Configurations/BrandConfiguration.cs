using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

public sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(b => b.Name)
        .HasConversion(b => b.Value, v => BrandName.Create(v).Value!);

        builder.Property(b => b.Slug)
        .HasConversion(b => b.Value, v => BrandSlug.Create(v).Value!);

        builder.HasMany(b => b.Products)
        .WithOne()
        .HasForeignKey(p => p.BrandId)
        .HasPrincipalKey(b => b.Id);
    }
}