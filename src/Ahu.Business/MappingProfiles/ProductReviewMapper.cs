using Ahu.Business.DTOs.ProductReviewDtos;
using Ahu.Core.Entities;
using Ahu.Core.Entities.Identity;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class ProductReviewMapper : Profile
{
    public ProductReviewMapper()
    {
        CreateMap<ProductReviewPostDto, ProductReview>().ReverseMap();
        CreateMap<ProductReview, ProductReviewGetDto>().ReverseMap();
        CreateMap<AppUser, UserInProductReviewGetDto>().ReverseMap();
        CreateMap<Product, ProductInProductReviewGetDto>().ReverseMap();
    }
}