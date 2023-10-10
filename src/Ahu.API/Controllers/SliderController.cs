using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.SliderDtos;
using Ahu.Business.Services.Interfaces;
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

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            return Ok(await _sliderService.GetAllSlidersAsync(search));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            return Ok(slider);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(SliderPostDto sliderPostDto)
        {
            var result = await _sliderService.CreateSliderAsync(sliderPostDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(result, HttpStatusCode.Created, "Slider successfully created"));
        }
    }
}
