using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.ProductId).IsRequired(true);
        builder.Property(x => x.Count).IsRequired(true);

        builder.HasOne(oi => oi.Order).WithMany(o => o.OrderItems);
    }
}