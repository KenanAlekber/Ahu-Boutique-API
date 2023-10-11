using Ahu.Core.Entities.Common;
using Ahu.Core.Entities.Identity;

namespace Ahu.Core.Entities;

public class BasketItem : BaseSectionEntity
{
    public Guid ProductId { get; set; }
    public string UserId { get; set; }
    public int Count { get; set; }
    public AppUser User { get; set; }
    public Product Product { get; set; }
}