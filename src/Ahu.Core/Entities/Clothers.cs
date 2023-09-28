using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class Clothers : BaseSectionEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Category { get; set; }
    public int? DiscountPercent { get; set; }
    public int Rating { get; set; }
    public int StockCount { get; set; }
}