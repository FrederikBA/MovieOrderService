using MovieOrders.Web.Model.Dto;

namespace MovieOrders.Web.Interface.DomainServices;

public interface IOrderService
{
    Task CreateOrderAsync(List<MovieDto> movies);
    Task<OrderDto> GetOrderAsync(int id);
    Task<List<OrderDto>> GetOrdersAsync();
}