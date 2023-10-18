using Ahu.Business.DTOs.StoreDataDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Helpers;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Ahu.Business.Services.Implementations;

public class StoreDataService : IStoreDataService
{
    private readonly IStoreDataRepository _storeDataRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StoreDataService(IStoreDataRepository storeDataRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _storeDataRepository = storeDataRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> CreateStoreData(StoreDataPostDto storeDataPost)
    {
        var storeData = _mapper.Map<StoreData>(storeDataPost);

        string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

        storeData.LogoImageName = FileManager.Save(storeDataPost.LogoImageFile, rootPath, "uploads/store-datas");
        storeData.LogoImageLink = "/uploads/store-datas/" + storeData.LogoImageName;
        storeData.EmptyBasketImageName = FileManager.Save(storeDataPost.EmptyBasketImageFile, rootPath, "uploads/store-datas");
        storeData.EmptyBasketImageLink = "/uploads/store-datas/" + storeData.EmptyBasketImageName;

        _storeDataRepository.Add(storeData);
        await _storeDataRepository.SaveAsync();

        return storeData.Id;
    }

    public void EditStoreData(StoreDataPutDto storeDataPutDto)
    {
        List<StoreData> storeDatas = _storeDataRepository.GetFiltered(x => true).ToList();

        if (storeDatas == null)
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Store Data not found");

        var dtoProperties = storeDataPutDto.GetType().GetProperties();
        var storeDataProperties = storeDatas.GetType().GetProperties();

        foreach (var dtoProperty in dtoProperties)
        {
            var entityProperty = storeDataProperties.FirstOrDefault(p => p.Name == dtoProperty.Name);

            if (entityProperty != null)
            {
                var dtoValue = dtoProperty.GetValue(storeDataPutDto);
                if (dtoValue != null)
                {
                    entityProperty.SetValue(storeDatas, dtoValue);
                }
            }
        }

        string? oldLogoImgName = null;
        string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

        if (oldLogoImgName != null)
            FileManager.Delete(rootPath, "uploads/store-datas", oldLogoImgName);

        foreach (var storeData in storeDatas)
        {
            if (storeDataPutDto.LogoImageFile != null)
            {
                oldLogoImgName = storeData.LogoImageName;
                storeData.LogoImageName = FileManager.Save(storeDataPutDto.LogoImageFile, rootPath, "uploads/store-datas");
                storeData.LogoImageLink = "/uploads/store-datas/" + storeData.LogoImageName;
            }
        }

        _storeDataRepository.SaveAsync();
    }

    public StoreDataGetDto GetStoreData()
    {
        List<StoreData> storeDatas = _storeDataRepository.GetAll(x => true).ToList();
        string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

        foreach (var storeData in storeDatas)
        {
            storeData.EmptyBasketImageLink = baseUrl + storeData.EmptyBasketImageLink;
            storeData.LogoImageLink = baseUrl + storeData.LogoImageLink;
        }

        return _mapper.Map<StoreDataGetDto>(storeDatas);
    }
}