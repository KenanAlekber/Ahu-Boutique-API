using Ahu.Business.DTOs.ProductReviewDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IProductReviewService
{
    Task<List<ProductReviewGetDto>> GetAllProductReviewsAsync();
    Task<ProductReviewGetDto> GetProductReviewAsync(Guid productId);
    Task<Guid> CreateProductReviewAsync(ProductReviewPostDto productReviewPostDto);
}