using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class ClotherImage : BaseSectionEntity
{
    public string Images { get; set; }
    public Clother Clother { get; set; }
}