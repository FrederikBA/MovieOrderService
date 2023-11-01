using Ardalis.Specification;

namespace MovieOrders.Web.Interface.Repository;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
    
}