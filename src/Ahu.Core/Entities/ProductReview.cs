using Ahu.Core.Entities.Common;
using Ahu.Core.Entities.Identity;

namespace Ahu.Core.Entities;

public class ProductReview : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string Answer { get; set; }
    public string Date { get; set; }
    public AppUser User { get; set; }
    public Product Product { get; set; }
}