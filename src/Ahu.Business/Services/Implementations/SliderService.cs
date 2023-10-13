﻿using Ahu.Business.DTOs.SliderDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Exceptions.SliderExceptions;
using Ahu.Business.Helpers;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class SliderService : ISliderService
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IMapper _mapper;

    public SliderService(ISliderRepository sliderRepository, IMapper mapper)
    {
        _sliderRepository = sliderRepository;
        _mapper = mapper;
    }

    public async Task<List<SliderGetDto>> GetAllSlidersAsync(string? search)
    {
        var sliders = await _sliderRepository.GetFiltered(s => search != null ? s.Title.Contains(search) : true).ToListAsync();

        List<SliderGetDto> sliderDtos = null;

        try
        {
            sliderDtos = _mapper.Map<List<SliderGetDto>>(sliders);
        }
        catch (SliderNotFoundException ex)
        {
            throw new SliderNotFoundException(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return sliderDtos;
    }

    public async Task<SliderGetDto> GetSliderByIdAsync(Guid id)
    {
        var slider = await _sliderRepository.GetSingleAsync(s => s.Id == id);

        if (slider == null)
            throw new SliderNotFoundException($"Slider is not found by id: {id}");

        var sliderDto = _mapper.Map<SliderGetDto>(slider);

        return sliderDto;
    }

    public async Task<Guid> CreateSliderAsync(SliderPostDto sliderPostDto)
    {
        List<RestExceptionError> errors = new List<RestExceptionError>();

        if (await _sliderRepository.IsExistAsync(x => x.Title == sliderPostDto.Title))
            errors.Add(new RestExceptionError("Title", "Title is already exists"));

        if (errors.Count > 0)
            throw new RestException(System.Net.HttpStatusCode.Conflict, errors);

        var slider = _mapper.Map<Slider>(sliderPostDto);

        string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

        slider.ImageName = FileManager.Save(sliderPostDto.ImageFile, rootPath, "uploads/files");
        slider.ImageUrl = "/uploads/files/" + slider.ImageName;

        _sliderRepository.Add(slider);
        _sliderRepository.SaveAsync();

        return slider.Id;
    }
}