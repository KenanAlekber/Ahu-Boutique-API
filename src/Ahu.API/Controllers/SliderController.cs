using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.SliderDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSliders()
        {
            return Ok(await _sliderService.GetAllSlidersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSliderById(Guid id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            return Ok(slider);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> CreateSlider([FromForm] SliderPostDto sliderPostDto)
        {
            var result = await _sliderService.CreateSliderAsync(sliderPostDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(result, HttpStatusCode.Created, "Slider successfully created"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            _sliderService.DeleteSlider(id);
            return NoContent();
        }
    }
}