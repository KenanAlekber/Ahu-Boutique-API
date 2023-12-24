using Ahu.Business.DTOs.BasketItemDtos;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketItemsController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly UserManager<AppUser> _userManager;

    public BasketItemsController(IBasketService basketService, UserManager<AppUser> userManager)
    {
        _basketService = basketService;
        _userManager = userManager;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
        return Ok(_basketService.GetAllBaskets(user.Id));
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToBasket(BasketPostDto basketPostDto)
    {
        await _basketService.AddToBasket(basketPostDto);
        return StatusCode(201, "Add to basket completed successfully");
    }

    [HttpPost("reduce")]
    public IActionResult ReduceBasket(BasketPostDto basketPostDto)
    {
        _basketService.ReduceBasketItem(basketPostDto);
        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _basketService.DeleteBasket(id);
        return NoContent();
    }
}