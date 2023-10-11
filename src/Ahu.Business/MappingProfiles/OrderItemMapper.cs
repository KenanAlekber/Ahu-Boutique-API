using Ahu.Business.DTOs.OrderDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class OrderItemMapper : Profile
{
    public OrderItemMapper()
    {
        CreateMap<OrderItem, OrderItemDto>();
    }
}