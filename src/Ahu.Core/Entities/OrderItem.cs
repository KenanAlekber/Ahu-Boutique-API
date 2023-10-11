using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class OrderItem : BaseSectionEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Count { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
}