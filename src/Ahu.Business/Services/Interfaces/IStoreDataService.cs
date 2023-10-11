using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.StoreDataDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IStoreDataService
{
    StoreDataGetDto GetStoreData();
    Task<Guid> CreateStoreData(StoreDataPostDto storeDataPostDto);
    void EditStoreData(StoreDataGetDto storeDataGetDto);
}