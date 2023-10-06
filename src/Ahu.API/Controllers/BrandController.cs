using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.BrandDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            return Ok(await _brandService.GetAllBrandsAsync(search));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var Brand = await _brandService.GetBrandByIdAsync(id);
            return Ok(Brand);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(BrandPostDto brandPostDto)
        {
            await _brandService.CreateBrandAsync(brandPostDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(HttpStatusCode.Created, "Brand successfully created"));
        }
    }
}