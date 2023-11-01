using MovieOrders.Web.Interface.DomainServices;
using MovieOrders.Web.Model.Dto;

namespace MovieOrders.Web.Service;

public class OrderService : IOrderService
{
    public Task<OrderDto> CreateOrderAsync(OrderDto order)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderDto>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }
}