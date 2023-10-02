using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class ShoeConfiguration
{
    public void Configure(EntityTypeBuilder<Shoe> builder)
    {
        builder.Property(sh => sh.Brand).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.Description).IsRequired(true).HasMaxLength(2500);
        builder.Property(sh => sh.Price).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.Size).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.Color).IsRequired(true).HasMaxLength(250);
        builder.Property(sh => sh.IsDeleted).HasDefaultValue(false);

        builder.HasMany(si => si.ShoeImages).WithOne(s => s.Shoe);
    }
}