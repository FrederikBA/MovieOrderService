using Ardalis.Specification;
using MovieOrders.Web.Model;

namespace MovieOrders.Web.Specifications;

public class OrdersAndOrderLinesSpec : Specification<Order>
{
    public OrdersAndOrderLinesSpec()
    {
        Query.Include(order => order.OrderLines);
    }
}