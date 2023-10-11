using Ahu.Business.DTOs.StoreDataDtos;
using Ahu.Core.Entities;
using AutoMapper;

namespace Ahu.Business.MappingProfiles;

public class StoreDataMapper : Profile
{
    public StoreDataMapper()
    {
        CreateMap<StoreDataPostDto, StoreData>().ReverseMap();
        CreateMap<StoreData, StoreDataGetDto>().ReverseMap();
    }
}