using Ahu.Business.DTOs.ProductDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductPostDto, Product>().ReverseMap();
        CreateMap<Product, ProductGetDto>().ReverseMap();
        CreateMap<Brand, BrandInProductGetDto>().ReverseMap();
        CreateMap<Category, CategoryInProductGetDto>().ReverseMap();
        CreateMap<ProductImage, ProductImagesInProductGetDto>().ReverseMap();
    }
}