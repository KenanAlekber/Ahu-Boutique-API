using Ahu.Business.DTOs.OrderDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Exceptions.OrderExceptions;
using Ahu.Business.Exceptions.ProductExceptions;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _orderRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<OrderGetDto>> GetAllOrdersAsync()
    {
        List<Order> orders = await _orderRepository.GetFiltered(p => true).ToListAsync();
        List<OrderGetDto> orderGetDtos = null;

        try
        {
            orderGetDtos = _mapper.Map<List<OrderGetDto>>(orders);
        }
        catch (ProductNotFoundException ex)
        {
            throw new OrderNotFoundException(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return orderGetDtos;
    }

    public async Task<OrderGetDto> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetSingleAsync(c => c.Id == id);

        if (order == null)
            throw new OrderNotFoundException($"Order is not found by id: {id}");

        var orderDto = _mapper.Map<OrderGetDto>(order);

        return orderDto;
    }

    public async Task<Guid> CreateOrderAsync(OrderPostDto orderPostDto)
    {
        Order order = _mapper.Map<Order>(orderPostDto);

        await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveAsync();

        return order.Id;
    }

    public void DeleteOrder(Guid id)
    {
        var order = _orderRepository.GetAll(x => true).FirstOrDefault(x => x.Id == id);

        if (order is null)
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Order not found");

        _orderRepository.Delete(order);
        _orderRepository.SaveAsync();
    }
}