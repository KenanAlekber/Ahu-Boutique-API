using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class Shoe : BaseSectionEntity
{
    public string Brand { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Size { get; set; }
    public string Color { get; set; }
    public string Category { get; set; }
    public int? DiscountPercent { get; set; }
    public int Rating { get; set; }
    public int StockCount { get; set; }
    public ICollection<ShoeImage>? ShoeImages { get; set; }
}