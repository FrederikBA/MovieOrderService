using MovieOrders.Web.Model.Dto;

namespace MovieOrders.Web.Interface.DomainServices;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(OrderDto order);
    Task<OrderDto> GetOrderAsync(int id);
    Task<List<OrderDto>> GetOrdersAsync();
}