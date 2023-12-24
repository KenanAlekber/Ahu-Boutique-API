using Ahu.Business.DTOs.BasketItemDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IBasketService
{
    List<BasketGetDto> GetAllBaskets(string userId);
    void ReduceBasketItem(BasketPostDto basketPostDto);
    Task AddToBasket(BasketPostDto basketPostDto);
    void DeleteBasket(Guid id);
}