using Ahu.Business.DTOs.ProductDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllProductsAsync();
    Task<ProductGetDto> GetProductByIdAsync(Guid id);
    Task<Guid> CreateProductAsync(ProductPostDto productPostDto);
    void Edit(ProductPutDto productPutDto);
    void DeleteProduct(Guid id);
}