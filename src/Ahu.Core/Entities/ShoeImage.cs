using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class ShoeImage : BaseSectionEntity
{
    public string Images { get; set; }
    public Shoe Shoe { get; set; }
}