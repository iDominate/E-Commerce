using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.Enums;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

public sealed class ProductConfiguration :
IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Slug)
        .HasConversion(s => s.Value, v => ProductSlug.Create(v).Value!);
        

        builder.Property(p => p.Name)
        .HasConversion(s => s.Value, v => ProductName.Create(v).Value!);

        builder.OwnsOne(p => p.Discount, discount =>
        {
            discount.Property(x => x.DiscountName)
            .HasColumnName("DiscountName");

            discount.Property(p => p.Description)
            .HasColumnName("DiscountDescription");

            discount.Property(p => p.Code)
            .HasColumnName("DiscountCode");
            
            discount.Property(p => p.Id)
            .HasColumnName("DiscountId");
            
            discount.Property(p => p.Calculation)
            .HasConversion(c => c.ToString(), v => Enum.Parse<Calculation>(v))
            .HasColumnName("Calculation");
        });

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(x => x.Amount)
            .HasColumnName("Price");

            price.Property(x => x.Currency)
            .HasColumnName("Currency")
            .HasConversion(c => c.ToString(), v => Enum.Parse<CurrenyEnum>(v));
        });

        
        builder.HasOne(p => p.Category)
        .WithMany()
        .HasPrincipalKey(c => c.Id)
        .HasForeignKey(p => p.CategoryId);

        builder.HasOne(p => p.Brand)
        .WithMany()
        .HasPrincipalKey(b => b.Id)
        .HasForeignKey(p => p.BrandId);


        builder.HasMany(p => p.TransactionHistory).
        WithOne()
        .HasForeignKey(t => t.ProductId)
        .HasPrincipalKey(p => p.Id);

        builder.HasMany(p => p.Images)
        .WithOne()
        .HasForeignKey(i => i.ProductId)
        .HasPrincipalKey(p => p.Id);
        builder.Navigation(p => p.Images)
        .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(p => p.TransactionHistory)
        .WithOne()
        .HasPrincipalKey(p => p.Id)
        .HasForeignKey(t => t.ProductId);
        builder.Navigation(p => p.TransactionHistory)
        .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}