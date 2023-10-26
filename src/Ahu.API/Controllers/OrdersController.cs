using Ahu.Business.DTOs.OrderDtos;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities.Identity;
using Ahu.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IEmailSender emailSender, UserManager<AppUser> userManager, IOrderService orderService, IOrderRepository orderRepository)
    {
        _emailSender = emailSender;
        _userManager = userManager;
        _orderService = orderService;
        _orderRepository = orderRepository;
    }

    [HttpPost("CreateOrder")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateOrder([FromForm] OrderPostDto orderPostDto)
    {
        if (User.Identity.IsAuthenticated)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user is not null)
                orderPostDto.UserId = user.Id;
        }

        var order = await _orderService.CreateOrderAsync(orderPostDto);

        if (
            order != null)
            _emailSender.Send(orderPostDto.Email, "Order Is Pending...", $"Dear {orderPostDto.FullName} Your order is pending, you will be notified after it is confirmed by the admins. Thank you for choosing us!");

        return StatusCode(201, order);
    }

    [HttpGet("All")]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await _orderService.GetAllOrdersAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        return Ok(await _orderService.GetOrderByIdAsync(id));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        _orderService.DeleteOrder(id);
        return NoContent();
    }
}