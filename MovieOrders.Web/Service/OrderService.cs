using MovieOrders.Web.Interface.DomainServices;
using MovieOrders.Web.Interface.Repository;
using MovieOrders.Web.Model;
using MovieOrders.Web.Model.Dto;

namespace MovieOrders.Web.Service;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;

    public OrderService(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task CreateOrderAsync(List<MovieDto> movies)
    {
        var order = new Order
        {
            OrderDate = DateTime.Now,
            OrderLines = movies.Select(movie => new OrderLine
            {
                MovieId = movie.Id,
                MovieTitle = movie.Title
            }).ToList()
        };
        
        // Save the order to the database
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
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