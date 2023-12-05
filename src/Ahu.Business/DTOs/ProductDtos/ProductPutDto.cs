using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.ProductDtos;

public class ProductPutDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public int Rate { get; set; }
    public string Name { get; set; }
    public decimal SalePrice { get; set; }
    public decimal CostPrice { get; set; }
    public string Description { get; set; }
    public decimal DiscountPercent { get; set; }
    public int StockCount { get; set; }
    public IFormFile? PosterImageFile { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
}