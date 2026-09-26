using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration
    : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name.Value)
        .IsRequired()
        .HasMaxLength(32);

        builder.HasIndex(p => p.Name.Value)
        .IsUnique();

        builder.Property(p => p.Slug.Value)
        .IsRequired();
    }
}