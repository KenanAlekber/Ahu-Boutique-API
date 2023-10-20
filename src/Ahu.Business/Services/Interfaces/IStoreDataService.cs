using Ahu.Business.DTOs.StoreDataDtos;

namespace Ahu.Business.Services.Interfaces;

public interface IStoreDataService
{
    Task<List<StoreDataGetDto>> GetStoreData();
    Task<Guid> CreateStoreData(StoreDataPostDto storeDataPostDto);
    void EditStoreData(StoreDataPutDto storeDataPutDto);
}