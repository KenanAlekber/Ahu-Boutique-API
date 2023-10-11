using Ahu.Business.DTOs.BasketItemDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Services.Interfaces;
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

    public List<BasketGetDto> GetAllBaskets(Guid userId)
    {
        //var baskets = _basketRepository.GetAll(b => b.UserId == userId, "Product");

        //return _mapper.Map<List<BasketGetDto>>(baskets);
        throw new NotImplementedException();
    }

    public void AddToBasket(BasketPostDto basketPostDto)
    {
        //var basket = _basketRepository.GetFiltered(b => b.ProductId == basketPostDto.ProductId && /*x.UserId == dto.UserId, "Product"*/);

        //if (basket != null)
        //{
        //    basket.Count++;
        //}
        //else
        //{
        //    basket = _mapper.Map<BasketItem>(basketPostDto);
        //    basket.Count = 1;
        //    basket.UserId = basketPostDto.UserId;
        //    _basketRepository.Add(basket);
        //}

        //_basketRepository.SaveAsync();
    }

    public void ReduceBasketItem(BasketPostDto basketPostDto)
    {
        List<RestExceptionError> errors = new List<RestExceptionError>();

        var basket = _basketRepository.GetFiltered(x => x.ProductId == basketPostDto.ProductId /*&& x.UserId == basketPostDto.UserId*/);

        if (basket == null)
            errors.Add(new RestExceptionError("ProductId", "ProductId is not correct"));

        if (errors.Count > 0) throw new RestException(System.Net.HttpStatusCode.BadRequest, errors);

        //if (basket.Count > 1)
        //{
        //    basket.Count--;
        //}
        //else
        //{
        //    _basketRepository.Delete(basket);
        //}

        _basketRepository.SaveAsync();
    }

    public void DeleteBasket(Guid id)
    {
        var basket = _basketRepository.GetFiltered(x => x.ProductId == id);

        if (basket is null) 
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Item not found");

        //_basketRepository.Delete(basket);
        _basketRepository.SaveAsync();
    }

    public void DeleteAllBaskets(Guid userId)
    {
        //var baskets = _basketRepository.GetAll(x => x.UserId == userId).ToList();

        //if (baskets == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Item not found");

        //foreach (var basket in baskets)
        //    _basketRepository.Delete(basket);

        //_basketRepository.SaveAsync();
    }
}