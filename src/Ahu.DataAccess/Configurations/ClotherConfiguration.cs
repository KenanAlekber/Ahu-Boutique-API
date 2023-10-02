using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class ClotherConfiguration
{
    public void Configure(EntityTypeBuilder<Clother> builder)
    {
        builder.Property(cl => cl.Name).IsRequired(true).HasMaxLength(250);
        builder.Property(cl => cl.Description).IsRequired(true).HasMaxLength(2500);
        builder.Property(cl => cl.Price).IsRequired(true).HasMaxLength(250);
        builder.Property(cl => cl.Size).IsRequired(true).HasMaxLength(250);
        builder.Property(cl => cl.Color).IsRequired(true).HasMaxLength(250);
        builder.Property(cl => cl.IsDeleted).HasDefaultValue(false);

        builder.HasMany(ci => ci.ClotherImages).WithOne(c => c.Clother);
    }
}