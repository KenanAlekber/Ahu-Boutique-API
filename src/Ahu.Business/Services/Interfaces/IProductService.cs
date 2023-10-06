using Ahu.Business.DTOs.ProductDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllProductsAsync(string? search);
    Task<ProductGetDto> GetProductByIdAsync(Guid id);
    Task CreateProductAsync(ProductPostDto productPostDto);
}