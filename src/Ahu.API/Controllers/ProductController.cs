using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.DTOs.ProductDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahu.API.Controllers
{
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
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            return Ok(await _productService.GetAllProductsAsync(search));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(ProductPostDto productPostDto)
        {
            await _productService.CreateProductAsync(productPostDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(HttpStatusCode.Created, "Product successfully created"));
        }
    }
}