using Ahu.Business.DTOs.BasketItemDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;

namespace Ahu.Business.Services.Implementations;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public List<BasketGetDto> GetAllBaskets(string userId)
    {
        var baskets = _basketRepository.GetAll(x => x.UserId == userId, "Product");
        return _mapper.Map<List<BasketGetDto>>(baskets);
    }

    //public async Task<List<BasketGetDto>> GetAllBaskets(string userId)
    //{
    //    //List<BasketItem> baskets = _basketRepository.GetAll(b => b.UserId == userId.ToString(), "Product").ToList();
    //    //var baskets = _basketRepository.GetAll(x => x.UserId == userId, "Product");


    //    var baskets = await _basketRepository.GetAll(p => true).ToListAsync();

    //    List<BasketGetDto> basketGetDtos = null;

    //    try
    //    {
    //        basketGetDtos = _mapper.Map<List<BasketGetDto>>(baskets);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }

    //    //return _mapper.Map<Task<List<BasketGetDto>>>(baskets);

    //    return basketGetDtos;
    //}

    public async void AddToBasket(BasketPostDto basketPostDto)
    {
        BasketItem basket = await _basketRepository.GetSingleAsync(b => b.ProductId == basketPostDto.ProductId && b.UserId == basketPostDto.UserId.ToString());

        if (basket is not null)
        {
            basket.Count++;
        }
        else
        {
            basket = _mapper.Map<BasketItem>(basketPostDto);
            basket.Count = 1;
            basket.UserId = basketPostDto.UserId.ToString();
            _basketRepository.Add(basket);
        }

        await _basketRepository.SaveAsync();
    }

    public async void ReduceBasketItem(BasketPostDto basketPostDto)
    {
        List<RestExceptionError> errors = new List<RestExceptionError>();

        BasketItem basket = await _basketRepository.GetSingleAsync(x => x.ProductId == basketPostDto.ProductId && x.UserId == basketPostDto.UserId.ToString());

        if (basket == null)
            errors.Add(new RestExceptionError("ProductId", "ProductId is not correct"));

        if (errors.Count > 0)
            throw new RestException(System.Net.HttpStatusCode.BadRequest, errors);

        if (basket.Count > 1)
        {
            basket.Count--;
        }
        else
        {
            _basketRepository.Delete(basket);
        }

        _basketRepository.SaveAsync();
    }

    public void DeleteBasket(Guid id)
    {
        BasketItem basket = _basketRepository.GetAll(x => true).FirstOrDefault(x => x.ProductId == id);

        if (basket is null)
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Item not found");

        _basketRepository.Delete(basket);
        _basketRepository.SaveAsync();
    }
}