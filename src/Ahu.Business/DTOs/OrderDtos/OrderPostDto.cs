namespace Ahu.Business.DTOs.OrderDtos;

public class OrderPostDto
{
    public string? UserId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Note { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}