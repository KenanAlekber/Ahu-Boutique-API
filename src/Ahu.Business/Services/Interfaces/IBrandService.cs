﻿using Ahu.Business.DTOs.BrandDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IBrandService
{
    Task<List<BrandGetDto>> GetAllBrandsAsync(string? search);
    Task<BrandGetDto> GetBrandByIdAsync(Guid id);
    Task CreateBrandAsync(BrandPostDto brandPostDto);
}