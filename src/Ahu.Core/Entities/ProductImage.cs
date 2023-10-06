using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class ProductImage : BaseSectionEntity
{
    public Guid ProductId { get; set; }
    public string ImageName { get; set; }
    public string ImageUrl { get; set; }
    public bool PosterStatus { get; set; }
    public Product Product { get; set; }
}