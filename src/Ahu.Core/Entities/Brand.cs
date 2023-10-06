using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class Brand : BaseSectionEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}