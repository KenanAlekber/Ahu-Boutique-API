using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.FullName).HasMaxLength(20).IsRequired(true);
        builder.Property(x => x.Address).HasMaxLength(100).IsRequired(true);
        builder.Property(x => x.Note).HasMaxLength(200);
        builder.Property(x => x.Email).HasMaxLength(50).IsRequired(true);
        builder.Property(x => x.Phone).HasMaxLength(20).IsRequired(true);

        builder.HasOne(i => i.AppUser).WithMany(u => u.Orders);
        builder.HasMany(o => o.OrderItems).WithOne(oi => oi.Order);
    }
}