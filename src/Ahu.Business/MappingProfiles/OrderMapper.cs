using Ahu.Business.DTOs.OrderDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<OrderPostDto, Order>().ReverseMap();
        CreateMap<Order, OrderGetDto>().ReverseMap();
    }
}