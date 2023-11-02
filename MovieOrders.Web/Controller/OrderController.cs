using Microsoft.AspNetCore.Mvc;
using MovieOrders.Web.Interface.DomainServices;
using MovieOrders.Web.Model.Dto;

namespace MovieOrders.Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersAsync()
    {
        var orders = await _orderService.GetOrdersAsync();
        return Ok(orders);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderAsync(int id)
    {
        var order = await _orderService.GetOrderAsync(id);
        return Ok(order);
    }
}