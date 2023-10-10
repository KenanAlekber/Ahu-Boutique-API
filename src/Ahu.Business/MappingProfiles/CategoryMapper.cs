using Ahu.Business.DTOs.CategoryDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryPostDto, Category>();
        CreateMap<Category, CategoryGetDto>();
    }
}