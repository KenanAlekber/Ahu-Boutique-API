using Ahu.Core.Entities;

namespace Ahu.Business.DTOs.BasketItemDtos;

public class BasketGetDto
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public string UserId { get; set; }
    public int Count { get; set; }
    public Product Product { get; set; }
}