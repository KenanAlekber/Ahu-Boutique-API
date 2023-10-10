using Ahu.Business.DTOs.BrandDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class BrandMapper : Profile
{
    public BrandMapper()
    {
        CreateMap<BrandPostDto, Brand>().ReverseMap();
        CreateMap<Brand, BrandGetDto>().ReverseMap();
    }
}