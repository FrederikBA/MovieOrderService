using Ardalis.Specification;
using MovieOrders.Web.Model;

namespace MovieOrders.Web.Specifications;

public class OrderAndOrderLinesSpec : Specification<Order>
{
    public OrderAndOrderLinesSpec(int orderId)
    {
        Query.Where(order => order.Id == orderId)
            .Include(order => order.OrderLines);
    }
}