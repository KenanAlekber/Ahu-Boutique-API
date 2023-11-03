using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.ProductDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productService.GetAllProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> CreateProduct([FromForm] ProductPostDto productPostDto)
    {
        var result = await _productService.CreateProductAsync(productPostDto);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDto(result, HttpStatusCode.Created, "Product successfully created"));
    }

    [HttpPut("Edit")]
    [Authorize(Roles = "Admin")]
    public ActionResult Update([FromForm] ProductPutDto productPutDto)
    {
        _productService.Edit(productPutDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        _productService.DeleteProduct(id);
        return NoContent();
    }
}