using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.ProductDtos;

public record ProductPostDto(Guid CategoryId, Guid BrandId, string Name, string Description, List<IFormFile> ImageFiles, IFormFile PosterImageFile,
    int Rating, decimal CostPrice, decimal SalePrice, string Color, string Size, int? DiscountPercent, int StockCount);