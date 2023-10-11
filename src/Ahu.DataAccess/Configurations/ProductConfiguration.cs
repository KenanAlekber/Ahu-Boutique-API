using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class ProductConfiguration
{
    public ProductConfiguration(EntityTypeBuilder<Product> builder)
    {
        builder.Property(sh => sh.Name).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.Description).IsRequired(true).HasMaxLength(2500);
        builder.Property(sh => sh.CostPrice).IsRequired(true).HasMaxLength(25);
        builder.Property(sh => sh.SalePrice).IsRequired(true).HasMaxLength(25);
        builder.Property(sh => sh.Size).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.Color).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.CategoryId).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.BrandId).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.IsDeleted).HasDefaultValue(false);

        builder.HasMany(p => p.ProductImages).WithOne(i => i.Product);
        builder.HasOne(p => p.Brand).WithMany(b => b.Products);
        builder.HasOne(p => p.Category).WithMany(c => c.Products);
    }
}
