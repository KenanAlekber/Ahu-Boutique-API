using Ahu.Business.DTOs.CategoryDtos;
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
    public async Task<IActionResult> Create(OrderPostDto orderPostDto)
    {
        if (User.Identity.IsAuthenticated)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
                orderPostDto.UserId = user.Id;
        }

        var order = _orderService.CreateOrderAsync(orderPostDto);

        if (order != null)
            _emailSender.Send(orderPostDto.Email, "Order Is Pending...", $"Dear {orderPostDto.FullName}  Your order is pending, you will be notified after it is confirmed by the admins. Thank you for choosing us!");

        return StatusCode(201, order);
    }

    [HttpGet("All")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllOrders()
    {
        return Ok(_orderService.GetAllOrdersAsync);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<CategoryGetDto> GetOrderById(Guid id)
    {
        return Ok(_orderService.GetOrderByIdAsync(id));
    }

    //[HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    //public IActionResult Delete(int id)
    //{
    //    _orderService.Delete(id);
    //    return NoContent();
    //}


    //[HttpPut("Edit")]
    //[Authorize(Roles = "Admin")]
    //public IActionResult Edit(OrderPostDto orderPostDto)
    //{

    //    _orderService.ed(orderPostDto);

    //    Order order = _orderRepository.Get(x => x.Id == putDto.Id);

    //    if (orderPostDto.Status == OrderStatus.Accepted)
    //    {
    //        _emailSender.Send(order.Email, "Order Is Accepted!", $"Dear {order.FullName}  Your order has been confirmed. Our staff will contact you. Thank you for choosing us!");
    //    }

    //    if (orderPostDto.Status == OrderStatus.Rejected)
    //    {
    //        _emailSender.Send(order.Email, "Order Is Rejected!", $"Dear {order.FullName}  Your order has been rejected. Thank you for choosing us!");
    //    }
    //    return NoContent();
    //}
}