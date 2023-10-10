using Ahu.Business.DTOs.CategoryDtos;

namespace Ahu.Business.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllCategorysAsync(string? search);
    Task<CategoryGetDto> GetCategoryByIdAsync(Guid id);
    Task<Guid> CreateCategoryAsync(CategoryPostDto categoryPostDto);
}