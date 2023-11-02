using Ardalis.Specification;
using MovieOrders.Web.Exceptions;
using MovieOrders.Web.Interface.DomainServices;
using MovieOrders.Web.Interface.Repository;
using MovieOrders.Web.Model;
using MovieOrders.Web.Model.Dto;
using MovieOrders.Web.Specifications;

namespace MovieOrders.Web.Service;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IReadRepository<Order> _orderReadRepository;

    public OrderService(IRepository<Order> orderRepository, IReadRepository<Order> orderReadRepository)
    {
        _orderRepository = orderRepository;
        _orderReadRepository = orderReadRepository;
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

    public async Task<OrderDto> GetOrderAsync(int id)
    {
        var orderSpec = new OrderAndOrderLinesSpec(id);

        var order = await _orderReadRepository.GetBySpecAsync(orderSpec);

        if (order != null)
        {
            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderLines = order.OrderLines.Select(orderLine => new OrderLineDto
                {
                    Id = orderLine.Id,
                    OrderId = orderLine.OrderId,
                    MovieId = orderLine.MovieId,
                    MovieTitle = orderLine.MovieTitle
                }).ToList()
            };

            return orderDto;
        }
        else
        {
            throw new OrderNotFoundException(id);
        }
    }

    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var spec = new OrdersAndOrderLinesSpec();
        
        var orders = await _orderReadRepository.ListAsync(spec);
        
        var orderDtos = orders.Select(order => new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            OrderLines = order.OrderLines.Select(orderLine => new OrderLineDto
            {
                Id = orderLine.Id,
                OrderId = orderLine.OrderId,
                MovieId = orderLine.MovieId,
                MovieTitle = orderLine.MovieTitle
            }).ToList()
        }).ToList();

        return orderDtos;
    }
}