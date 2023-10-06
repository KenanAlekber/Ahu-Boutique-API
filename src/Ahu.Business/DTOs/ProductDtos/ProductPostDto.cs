namespace Ahu.Business.DTOs.ProductDtos;

public record ProductPostDto(string Name, string Description, string? Image, decimal CostPrice, decimal SalePrice, string Color, string Size,
    int Rating, int? DiscountPercent, int StockCount, Guid CategoryId, Guid BrandId);