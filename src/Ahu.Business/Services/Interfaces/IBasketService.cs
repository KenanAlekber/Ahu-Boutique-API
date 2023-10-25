using Ahu.Business.DTOs.BasketItemDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IBasketService
{
    void ReduceBasketItem(BasketPostDto basketPostDto);
    void AddToBasket(BasketPostDto basketPostDto);
    List<BasketGetDto> GetAllBaskets(string userId);
    //void DeleteBasket(Guid id);
    void DeleteAllBaskets(string userId);
}