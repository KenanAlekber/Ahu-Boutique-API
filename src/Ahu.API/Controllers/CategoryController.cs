using Ahu.Business.DTOs.CategoryDtos;
using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllCategories([FromQuery] string? search)
    {
        return Ok(await _categoryService.GetAllCategorysAsync(search));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateCategory(CategoryPostDto categoryPostDto)
    {
        var result = await _categoryService.CreateCategoryAsync(categoryPostDto);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDto(result, HttpStatusCode.Created,"Category successfully created"));
    }
}