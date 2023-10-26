using Ahu.Business.DTOs.BrandDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Exceptions.BrandExceptions;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<List<BrandGetDto>> GetAllBrandsAsync()
    {
        var brands = await _brandRepository.GetAll(p => true).ToListAsync();

        List<BrandGetDto> brandDtos = null;

        try
        {
            brandDtos = _mapper.Map<List<BrandGetDto>>(brands);
        }
        catch (BrandNotFoundException ex)
        {
            throw new BrandNotFoundException(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return brandDtos;
    }

    public async Task<BrandGetDto> GetBrandByIdAsync(Guid id)
    {
        var brand = await _brandRepository.GetSingleAsync(c => c.Id == id, "Brand");

        if (brand == null)
            throw new BrandNotFoundException($"Brand is not found by id: {id}");

        var brandDto = _mapper.Map<BrandGetDto>(brand);

        return brandDto;
    }

    public async Task<Guid> CreateBrandAsync(BrandPostDto brandPostDto)
    {
        Brand brand = _mapper.Map<Brand>(brandPostDto);

        await _brandRepository.CreateAsync(brand);
        await _brandRepository.SaveAsync();
        return brand.Id;
    }

    public void DeleteBrand(Guid id)
    {
        Brand brand = _brandRepository.GetAll(x => true).FirstOrDefault(x => x.Id == id);

        if (brand is null) 
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Brand not found");

        _brandRepository.Delete(brand);
        _brandRepository.SaveAsync();
    }
}