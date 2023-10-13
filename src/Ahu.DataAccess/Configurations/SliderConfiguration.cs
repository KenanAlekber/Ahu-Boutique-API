using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(s => s.Title).HasMaxLength(20).IsRequired(false);
        builder.Property(s => s.Description).HasMaxLength(150).IsRequired(false);
        builder.Property(s => s.ImageName).HasMaxLength(100).IsRequired(true);
    }
}