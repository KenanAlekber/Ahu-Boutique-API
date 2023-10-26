using Ahu.Business.DTOs.BrandDtos;
using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers;

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
    public async Task<IActionResult> GetAllBrands()
    {
        return Ok(await _brandService.GetAllBrandsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(Guid id)
    {
        var brand = await _brandService.GetBrandByIdAsync(id);
        return Ok(brand);
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> CreateBrand(BrandPostDto brandPostDto)
    {
        var result = await _brandService.CreateBrandAsync(brandPostDto);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDto(result, HttpStatusCode.Created, "Brand successfully created"));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        _brandService.DeleteBrand(id);
        return NoContent();
    }
}