namespace Ahu.Business.DTOs.OrderDtos;

public record OrderPostDto(Guid? UserId, string Fullname, string Phone, string Address, string Email, string Note, List<OrderItemDto> OrderItems);