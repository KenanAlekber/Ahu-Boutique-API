using Ahu.Business.DTOs.BasketItemDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IBasketService
{
    List<BasketGetDto> GetAllBaskets(string userId);
    void ReduceBasketItem(BasketPostDto basketPostDto);
    void AddToBasket(BasketPostDto basketPostDto);
    void DeleteBasket(Guid id);
}