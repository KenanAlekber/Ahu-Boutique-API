using Ahu.Business.DTOs.BrandDtos;
using Ahu.Core.Entities;

namespace Ahu.Business.Services.Interfaces;

public interface IBrandService
{
    Task<List<BrandGetDto>> GetAllBrandsAsync();
    Task<BrandGetDto> GetBrandByIdAsync(Guid id);
    Task<Guid> CreateBrandAsync(BrandPostDto brandPostDto);
}