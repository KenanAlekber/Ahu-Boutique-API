using Ahu.Business.DTOs.BasketItemDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class BasketMapper : Profile
{
    public BasketMapper()
    {
        CreateMap<BasketPostDto, BasketItem>().ReverseMap();
        CreateMap<BasketItem, BasketGetDto>().ReverseMap();
    }
}