using Ahu.Core.Enums;

namespace Ahu.Business.DTOs.OrderDtos;

public record OrderGetDto(Guid Id, string Fullname, string Phone, string Address, string Email, string Note, DateTime CreatedAt, OrderStatus Status,
    List<OrderItemDto> OrderItems);