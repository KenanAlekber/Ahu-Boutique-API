using Ahu.Business.DTOs.SliderDtos;

namespace Ahu.Business.Services.Interfaces;

public interface ISliderService
{
    Task<List<SliderGetDto>> GetAllSlidersAsync(string? search);
    Task<SliderGetDto> GetSliderByIdAsync(Guid id);
    Task<Guid> CreateSliderAsync(SliderPostDto sliderPostDto);
}