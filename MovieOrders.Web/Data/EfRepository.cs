using Ardalis.Specification.EntityFrameworkCore;
using MovieOrders.Web.Interface.Repository;

namespace MovieOrders.Web.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public readonly OrderContext OrderContext;

    public EfRepository(OrderContext orderContext) : base(orderContext) =>
        this.OrderContext = orderContext;
}