using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class Product : BaseSectionEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal CostPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public int StockCount { get; set; }
    public int Rate { get; set; }
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
    public Brand Brand { get; set; }
    public Guid BrandId { get; set; }
    public List<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}