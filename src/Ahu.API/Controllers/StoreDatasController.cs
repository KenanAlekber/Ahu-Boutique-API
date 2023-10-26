using Ahu.Business.DTOs.StoreDataDtos;
using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreDatasController : ControllerBase
{
    private readonly IStoreDataService _dataService;

    public StoreDatasController(IStoreDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStoreData([FromForm] StoreDataPostDto storeDataPostDto)
    {
        return StatusCode(201, await _dataService.CreateStoreData(storeDataPostDto));
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetStoreData()
    {
        return StatusCode(201, await _dataService.GetStoreData());
    }

    [HttpPut("Edit/{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult EditStoreData([FromForm] StoreDataPutDto storeDataPutDto)
    {
        _dataService.EditStoreData(storeDataPutDto);
        return StatusCode(200, storeDataPutDto);
    }
}