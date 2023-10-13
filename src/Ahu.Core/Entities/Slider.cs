using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class Slider : BaseSectionEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public string ImageUrl { get; set; }
}