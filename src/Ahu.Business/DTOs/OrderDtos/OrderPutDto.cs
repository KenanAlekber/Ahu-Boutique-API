using Ahu.Core.Enums;

namespace Ahu.Business.DTOs.OrderDtos;

public class OrderPutDto
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}