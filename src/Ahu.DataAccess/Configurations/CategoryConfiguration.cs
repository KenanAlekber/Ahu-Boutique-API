using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ahu.DataAccess.Configurations;

public class CategoryConfiguration
{
    public CategoryConfiguration(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name).IsRequired(true).HasMaxLength(250);

        builder.HasMany(cr => cr.Products).WithOne(c => c.Category);
    }
}