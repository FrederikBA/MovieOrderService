using Ardalis.Specification;

namespace MovieOrders.Web.Interface.Repository;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    
}