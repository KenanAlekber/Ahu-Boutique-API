using Ahu.Business.DTOs.OrderDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderGetDto>> GetAllOrdersAsync();
    Task<OrderGetDto> GetOrderByIdAsync(Guid id);
    Task<Guid> CreateOrderAsync(OrderPostDto orderPostDto);
    void DeleteOrder(Guid id);
}