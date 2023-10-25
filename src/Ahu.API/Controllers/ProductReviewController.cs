using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.ProductReviewDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductReviewController : ControllerBase
{
    private readonly IProductReviewService _productReviewService;

    public ProductReviewController(IProductReviewService productReviewService)
    {
        _productReviewService = productReviewService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productReviewService.GetAllProductReviewsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productReviewService.GetProductReviewAsync(id);
        return Ok(product);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProduct(ProductReviewPostDto productReviewPostDto)
    {
        var result = await _productReviewService.CreateProductReviewAsync(productReviewPostDto);
        return StatusCode((int)HttpStatusCode.Created, new ResponseDto(result, HttpStatusCode.Created, "Product Review successfully created"));
    }
}

//{
//  "userId": "062cc1d0-00b3-40db-8705-728482ebcd1b",
//  "productId": "304d7e01-a009-4ebc-80eb-08dbcbe0e3e8",
//  "description": "Test Desc",
//  "raiting": 3
//}