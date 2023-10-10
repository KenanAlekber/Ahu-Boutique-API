using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class Category : BaseSectionEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}