using Ahu.Business.DTOs.SliderDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class SliderMapper : Profile
{
    public SliderMapper()
    {
        CreateMap<SliderPostDto, Slider>().ReverseMap();
        CreateMap<Slider, SliderGetDto>().ReverseMap();
    }
}