using Ahu.Business.DTOs.BasketItemDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IBasketService
{
    void ReduceBasketItem(BasketPostDto basketPostDto);
    void AddToBasket(BasketPostDto basketPostDto);
    List<BasketGetDto> GetAllBaskets(Guid userId);
    void DeleteBasket(Guid id);
    void DeleteAllBaskets(Guid userId);
}