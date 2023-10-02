namespace Ahu.Core.Entities.Common;

public abstract class BaseSectionEntity : BaseEntity
{
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedTime { get; set;}
    public string UpdatedBy { get; set;}
}