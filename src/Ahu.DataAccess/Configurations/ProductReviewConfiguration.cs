using Ahu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahu.DataAccess.Configurations;

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.Property(pr => pr.UserId).IsRequired(true);
        builder.Property(pr => pr.ProductId).IsRequired(true);
        builder.Property(pr => pr.Description).IsRequired(true).HasMaxLength(2500);

        builder.HasOne(item => item.User).WithMany(user => user.ProductReviews);
        builder.HasOne(item => item.Product).WithMany(product => product.ProductReviews);
    }
}