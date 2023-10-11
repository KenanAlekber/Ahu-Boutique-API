using Ahu.Core.Entities.Common;
using Ahu.Core.Entities.Identity;
using Ahu.Core.Enums;

namespace Ahu.Core.Entities;

public class Order : BaseSectionEntity
{
    public Guid? UserId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public AppUser? AppUser { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}