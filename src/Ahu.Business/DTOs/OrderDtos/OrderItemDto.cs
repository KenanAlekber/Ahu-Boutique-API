namespace Ahu.Business.DTOs.OrderDtos;

public record OrderItemDto(Guid ProductId, string ProductName, int Count);