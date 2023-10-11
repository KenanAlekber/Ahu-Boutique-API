using Ahu.Core.Entities;

namespace Ahu.Business.DTOs.BasketItemDtos;

public record BasketGetDto(Guid Id, Guid ProductId, Guid UseId, int Count, Product Product);