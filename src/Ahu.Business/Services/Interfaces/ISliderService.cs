using Ahu.Business.DTOs.SliderDtos;

namespace Ahu.Business.Services.Interfaces;

public interface ISliderService
{
    Task<List<SliderGetDto>> GetAllSlidersAsync();
    Task<SliderGetDto> GetSliderByIdAsync(Guid id);
    Task<Guid> CreateSliderAsync(SliderPostDto sliderPostDto);
    void DeleteSlider(Guid id);
}