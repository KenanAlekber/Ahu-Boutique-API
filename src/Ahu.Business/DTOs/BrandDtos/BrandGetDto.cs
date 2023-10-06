using Ahu.Core.Entities;

namespace Ahu.Business.DTOs.BrandDtos;

public record BrandGetDto(Guid Id, string Name, List<Product> Products);